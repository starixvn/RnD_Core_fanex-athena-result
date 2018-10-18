namespace Fanex.Result.Core.Sportsbook.ExcelModels
{
    using System.Collections.Generic;
    using Base;
    using Base.ExcelModels;
    using Base.Extension;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using NPOI.SS.UserModel;
    using ViewModels;

    internal sealed class ESportExcel : ExcelBase, IExcelModel
    {
        public ESportExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            ColumnTemplateCount = 14;
            ColumnStatus = 13;
            RowContentTemplate = 3;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(LangResource.MatchCode + "/" + LangResource.ID);
            row.Cells[colidx++].SetCellValue(LangResource.KickOffTime);
            row.Cells[colidx++].SetCellValue(LangResource.Match);

            for (int i = 0; i < ESportViewModel.MaxNumberOfMaps; i++)
            {
                row.Cells[colidx++].SetCellValue($"M{i + 1}");
            }

            row.Cells[colidx++].SetCellValue(LangResource.FinalScore);
            row.Cells[colidx].SetCellValue(LangResource.Status);
        }

        #region Assign Cell Data

        protected override void SetDataForMatch(ISheet sheet, IResultViewModel data, ref int currentRow)
        {
#pragma warning disable S3215 // "interface" instances should not be cast to concrete types
            var exportRow = (ESportViewModel)data;
#pragma warning restore S3215 // "interface" instances should not be cast to concrete types

            var rowHome = sheet.CopyRow(TemplateSheet, GetRowTeamWin(exportRow.HomeWin), currentRow);
            var rowAway = sheet.CopyRow(TemplateSheet, GetRowTeamWin(exportRow.AwayWin), currentRow + 1);

            MergeResultCells(sheet, currentRow, currentRow + 1, ColumnKickOffTime, ColumnStatus);

            AssignStringData(
                rowHome,
                ColumnMatchCode,
                exportRow.MatchCode,
                Helper.ToRtfString(exportRow.KickOffTime),
                Helper.ToRtfString(exportRow.HomeTeam),
                exportRow.GetHomeAwaySet(0, true),
                exportRow.GetHomeAwaySet(1, true),
                exportRow.GetHomeAwaySet(2, true),
                exportRow.GetHomeAwaySet(3, true),
                exportRow.GetHomeAwaySet(4, true),
                exportRow.GetHomeAwaySet(5, true),
                exportRow.GetHomeAwaySet(6, true),
                exportRow.GetHomeAwaySet(7, true),
                exportRow.GetHomeAwaySet(8, true),
                exportRow.FinalHomeScore,
                exportRow.EventStatus);
            currentRow++;

            AssignStringData(
                rowAway,
                ColumnMatchCode,
                exportRow.MatchId,
                Helper.ToRtfString(exportRow.KickOffTime),
                Helper.ToRtfString(exportRow.AwayTeam),
                exportRow.GetHomeAwaySet(0, false),
                exportRow.GetHomeAwaySet(1, false),
                exportRow.GetHomeAwaySet(2, false),
                exportRow.GetHomeAwaySet(3, false),
                exportRow.GetHomeAwaySet(4, false),
                exportRow.GetHomeAwaySet(5, false),
                exportRow.GetHomeAwaySet(6, false),
                exportRow.GetHomeAwaySet(7, false),
                exportRow.GetHomeAwaySet(8, false),
                exportRow.FinalAwayScore,
                exportRow.EventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}