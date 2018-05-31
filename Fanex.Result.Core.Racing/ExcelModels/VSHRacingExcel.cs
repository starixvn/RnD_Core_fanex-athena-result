namespace Fanex.Result.Core.Racing.ExcelModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.ExcelModels;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Racing.ViewModels;
    using NPOI.SS.UserModel;

    internal sealed class VSHRacingExcel : ExcelBase, IExcelModel
    {
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public VSHRacingExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            this.ColumnTemplateCount = 7;
            this.ColumnStatus = 6;
            this.RowContentTemplate = 3;
            this.IsSupportMatchCode = false;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 3;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[0].SetCellValue(LangResource.Race);
            row.Cells[colidx++].SetCellValue(LangResource.Results);
            row.Cells[colidx++].SetCellValue(LangResource.Win);
            row.Cells[colidx++].SetCellValue(LangResource.Place);
            row.Cells[colidx].SetCellValue(LangResource.Status);
        }

        /// <summary>
        /// Assign data to sheet.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        protected override void AssignSheetData(ISheet sheet)
        {
            var currentRow = RowLeagueTemplate;
            var currentLeagueId = 0;
            var endRowSpan = 3;
            var groupData = this.ViewModelData.GroupBy(i => i.LeagueId).ToList();
            foreach (var item in groupData)
            {
                foreach (VSHRacingViewModel exportRow in item.ToList())
                {
                    if (exportRow.Place > 4)
                    {
                        continue;
                    }

                    SetMatch(sheet, exportRow, ref currentLeagueId, ref currentRow, ref endRowSpan);
                }
            }
        }

        #region Assign Cell Data

        private static void MergeKickOffTimeCell(ISheet sheet, int currentRow, int rowSpan, int colSpanKichOffTime)
        {
            sheet.MergeCells(currentRow, colSpanKichOffTime, rowSpan, colSpanKichOffTime);
        }

        /// <summary>
        /// Set data for match row.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        /// <param name="exportRow">The view model.</param>
        /// <param name="currentLeagueId">The current league id.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="endRowSpan">The end row span.</param>
        private void SetMatch(ISheet sheet, VSHRacingViewModel exportRow, ref int currentLeagueId, ref int currentRow, ref int endRowSpan)
        {
            var cellIndex = 0;

            var leagueId = exportRow.LeagueId;
            var currentRace = DateTime.MinValue;
            SetLeague(sheet, exportRow.LeagueId, exportRow.LeagueName, ref currentLeagueId, ref currentRow);

            if (currentLeagueId == 0 || currentLeagueId != leagueId ||
                currentRace != exportRow.EvenTime)
            {
                currentRace = exportRow.EvenTime;

                // Calculate row span
                var rowsTopRace =
                this.ViewModelData.Where(i => i.LeagueId == leagueId)
                .Select(i => (VSHRacingViewModel)i)
                .Count(i => i.EvenTime == currentRace && i.Place < 5);

                if (currentRow == 3 || endRowSpan < currentRow)
                {
                    endRowSpan = currentRow + rowsTopRace - 1;
                    MergeKickOffTimeCell(sheet, currentRow, endRowSpan, colSpanKichOffTime: ColumnRace);
                }
            }

            var rowHome = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow);
            this.AssignStringData(
                rowHome,
                cellIndex,
                Helper.ToRtfString(exportRow.KickOffTime),
                exportRow.Placing,
                exportRow.HorseNo,
                exportRow.Result,
                exportRow.WinOdd,
                exportRow.PlaceOdd,
                exportRow.Status);

            currentRow++;
        }

        #endregion Assign Cell Data
    }
}