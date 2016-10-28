namespace Fanex.Result.Core.Sportsbook.ExcelModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Base;
    using Base.ExcelModels;
    using Base.Extension;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using NPOI.SS.UserModel;
    using ViewModels;

    internal sealed class TennisExcel : ExcelBase, IExcelModel
    {
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public TennisExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            this.ColumnTemplateCount = 11;
            this.ColumnStatus = 10;
            this.RowContentTemplate = 3;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(LangResource.MatchCode + "/" + LangResource.ID);

            row.Cells[colidx++].SetCellValue(LangResource.KickOffTime);
            row.Cells[colidx++].SetCellValue(LangResource.Match);
            row.Cells[colidx++].SetCellValue(LangResource.Lbl1S);
            row.Cells[colidx++].SetCellValue(LangResource.Lbl2S);
            row.Cells[colidx++].SetCellValue(LangResource.Lbl3S);
            row.Cells[colidx++].SetCellValue(LangResource.Lbl4S);
            row.Cells[colidx++].SetCellValue(LangResource.Lbl5S);
            row.Cells[colidx++].SetCellValue(LangResource.TotalGames);
            row.Cells[colidx++].SetCellValue(LangResource.TotalSet);
            row.Cells[colidx].SetCellValue(LangResource.Status);
        }

        #region Assign Cell Data

        protected override void SetDataForMatch(ISheet sheet, IResultViewModel data, ref int currentRow)
        {
            var exportRow = (TennisViewModel)data;
            var rowHome = sheet.CopyRow(TemplateSheet, this.GetRowTeamWin(exportRow.HomeWin), currentRow);

            var rowAway = sheet.CopyRow(TemplateSheet, this.GetRowTeamWin(exportRow.AwayWin), currentRow + 1);

            MergeResultCells(sheet, currentRow, currentRow + 1, ColumnKickOffTime, this.ColumnStatus);

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
                exportRow.TotalGameHome,
                exportRow.TotalSetHome,
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
                exportRow.TotalGameAway,
                exportRow.TotalSetAWay,
                exportRow.EventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}