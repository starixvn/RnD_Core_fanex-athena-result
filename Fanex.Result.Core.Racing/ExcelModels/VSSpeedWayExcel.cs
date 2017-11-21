namespace Fanex.Result.Core.Racing.ExcelModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.ExcelModels;
    using Base.Extension;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using NPOI.SS.UserModel;
    using ViewModels;

    internal sealed class VSSpeedWayExcel : ExcelBase, IExcelModel
    {
        public VSSpeedWayExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            ColumnTemplateCount = 6;
            ColumnStatus = 5;
            RowContentTemplate = 3;
            this.IsSupportMatchCode = false;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 3;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[ColumnRace].SetCellValue(LangResource.Race);
            row.Cells[colidx++].SetCellValue(LangResource.Results);
            row.Cells[colidx++].SetCellValue(LangResource.Win);
            row.Cells[colidx].SetCellValue(LangResource.Status);
        }

        protected override void AssignSheetData(ISheet sheet)
        {
            var currentRow = RowLeagueTemplate;
            var currentLeagueId = 0;
            var endRowSpan = 3;
            var groupData = ViewModelData.GroupBy(i => i.LeagueId).ToList();
            foreach (var item in groupData)
            {
                foreach (var exportRow in item.ToList().OfType<VSSpeedWayViewModel>())
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

        private void SetMatch(ISheet sheet, VSSpeedWayViewModel exportRow, ref int currentLeagueId, ref int currentRow, ref int endRowSpan)
        {
            var cellIndex = 0;

            var leagueId = exportRow.LeagueId;
            var currentRace = DateTime.MinValue;
            SetLeague(sheet, exportRow.LeagueId, exportRow.LeagueName, ref currentLeagueId, ref currentRow);

            if (currentLeagueId == 0 ||
                currentLeagueId != leagueId ||
                currentRace != exportRow.EvenTime)
            {
                currentRace = exportRow.EvenTime;

                // Calculate row span
                var rowsTopRace =
                ViewModelData.Where(i => i.LeagueId == leagueId)
#pragma warning disable S3215 // "interface" instances should not be cast to concrete types
                .Select(i => (VSSpeedWayViewModel)i).Count(i => i.EvenTime == currentRace && i.Place < 5);
#pragma warning restore S3215 // "interface" instances should not be cast to concrete types

                if (currentRow == 3 || endRowSpan < currentRow)
                {
                    endRowSpan = currentRow + rowsTopRace - 1;
                    MergeKickOffTimeCell(sheet, currentRow, endRowSpan, colSpanKichOffTime: ColumnRace);
                }
            }

            var rowHome = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow);
            AssignStringData(
                rowHome,
                cellIndex,
                Helper.ToRtfString(exportRow.KickOffTime),
                exportRow.Placing,
                exportRow.HorseNo,
                exportRow.Result,
                exportRow.WinOdd,
                exportRow.Status);

            currentRow++;
        }

        #endregion Assign Cell Data
    }
}