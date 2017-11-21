namespace Fanex.Result.Core.Racing.ExcelModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.ExcelModels;
    using Base.Extension;
    using Base.Resources;
    using Base.ViewModels;
    using NPOI.SS.UserModel;
    using ViewModels;

    internal sealed class RacingExcel : ExcelBase, IExcelModel
    {
        private readonly string _formatStatus;

        public RacingExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            ColumnTemplateCount = 7;
            ColumnStatus = 6;
            RowContentTemplate = 3;
            _formatStatus = LangResource.Official + " {0} {1}";
            this.IsSupportMatchCode = false;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 3;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[ColumnRace].SetCellValue(LangResource.Race);
            row.Cells[colidx++].SetCellValue(LangResource.Results);
            row.Cells[colidx++].SetCellValue(LangResource.Win);
            row.Cells[colidx++].SetCellValue(LangResource.Place);
            row.Cells[colidx].SetCellValue(LangResource.Status);
        }

        protected override void AssignSheetData(ISheet sheet)
        {
            var currentRow = 2;
            var currentLeagueId = 0;
            var endRowSpan = 3;
            var groupData = ViewModelData.GroupBy(i => i.LeagueId).ToList();

            foreach (var item in groupData)
            {
                foreach (var exportRow in item.ToList().OfType<RacingViewModel>())
                {
                    if (exportRow.Place > 4)
                    {
                        continue;
                    }

                    SetMatch(sheet, exportRow, ref currentLeagueId, ref currentRow, ref endRowSpan);
                }
            }
        }

        protected override void ProcessUI(ISheet sheet)
        {
            /*
             * Void processing UI
             */
        }

        #region Assign Cell Data

        private void SetMatch(ISheet sheet, RacingViewModel exportRow, ref int currentLeagueId, ref int currentRow, ref int endRowSpan)
        {
            var cellIndex = 0;
            var status = string.Empty;
            var leagueId = exportRow.LeagueId;
            var currentRace = string.Empty;
            SetLeague(sheet, exportRow.LeagueId, exportRow.LeagueName, ref currentLeagueId, ref currentRow);

            if (currentLeagueId == 0 ||
                currentLeagueId != leagueId ||
                !currentRace.Equals(exportRow.RaceName))
            {
                currentRace = exportRow.RaceName;

                // Calculate row span
                var rowsTopRace = ViewModelData.Where(i => i.LeagueId == leagueId)
#pragma warning disable S3215 // "interface" instances should not be cast to concrete types
                    .Select(i => (RacingViewModel)i)
#pragma warning restore S3215 // "interface" instances should not be cast to concrete types
                    .Count(i => i.RaceName.Equals(currentRace) && i.Place < 5);

                if (currentRow == 3 || endRowSpan < currentRow)
                {
                    endRowSpan = currentRow + rowsTopRace - 1;
                    MergeResultCells(sheet, currentRow, endRowSpan, ColumnRace, ColumnStatus);
                }

                status = string.Format(
                    _formatStatus,
                    string.IsNullOrEmpty(exportRow.SCR) ? string.Empty : "\n" + LangResource.SCR + ":" + exportRow.SCR,
                    string.IsNullOrEmpty(exportRow.TF) ? string.Empty : "\n" + LangResource.TF + ":" + exportRow.TF);
            }

            var rowHome = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow);

            AssignStringData(
                rowHome,
                cellIndex,
                currentRace,
                exportRow.Placing,
                exportRow.HorseNo,
                exportRow.Result,
                exportRow.WinOdd,
                exportRow.PlaceOdd,
                status);

            currentRow++;
        }

        #endregion Assign Cell Data
    }
}