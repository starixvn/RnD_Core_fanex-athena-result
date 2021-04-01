namespace Fanex.Result.Core.Sportsbook.ExcelModels
{
    using Base;
    using Base.ExcelModels;
    using Base.Extension;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using NPOI.SS.UserModel;
    using System.Collections.Generic;
    using ViewModels;

    internal sealed class CricketExcel : ExcelBase, IExcelModel
    {
        public CricketExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            ColumnTemplateCount = 7;
            ColumnStatus = 6;
            RowContentTemplate = 3;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(LangResource.MatchCode + "/" + LangResource.ID);
            row.Cells[colidx++].SetCellValue(LangResource.KickOffTime);
            row.Cells[colidx++].SetCellValue(LangResource.Match);
            row.Cells[colidx++].SetCellValue(LangResource.Inns1);
            row.Cells[colidx++].SetCellValue(LangResource.Inns2);
            row.Cells[colidx++].SetCellValue(LangResource.FinalScore);
            row.Cells[colidx].SetCellValue(LangResource.Status);
        }

        #region Assign Cell Data

        protected override void SetDataForMatch(ISheet sheet, IResultViewModel data, ref int currentRow)
        {
#pragma warning disable S3215 // "interface" instances should not be cast to concrete types
            var exportRow = (CricketViewModel)data;
#pragma warning restore S3215 // "interface" instances should not be cast to concrete types

            var rowHome = sheet.CopyRow(TemplateSheet, GetRowTeamWin(exportRow.HomeWin), currentRow);
            var rowAway = sheet.CopyRow(TemplateSheet, GetRowTeamWin(exportRow.AwayWin), currentRow + 1);

            MergeResultCells(sheet, currentRow, currentRow + 1, ColumnKickOffTime, ColumnStatus);
            var eventStatus = exportRow.EventStatus;

            AssignStringData(
                rowHome,
                ColumnMatchCode,
                exportRow.MatchCode,
                Helper.ToRtfString(exportRow.KickOffTime),
                exportRow.HomeTeam,
                exportRow.HomeFirstRuns,
                exportRow.HomeSecondRuns,
                exportRow.HomeFinalRuns,
                eventStatus);
            currentRow++;

            AssignStringData(
                rowAway,
                ColumnMatchCode,
                exportRow.MatchId,
                Helper.ToRtfString(exportRow.KickOffTime),
                exportRow.AwayTeam,
                exportRow.AwayFirstRuns,
                exportRow.AwaySecondRuns,
                exportRow.AwayFinalRuns,
                eventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}