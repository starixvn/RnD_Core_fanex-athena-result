namespace Fanex.Result.Core.Sportsbook.ExcelModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.ExcelModels;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Sportsbook.ViewModels;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Badminton excel model class.
    /// </summary>
    internal sealed class BadmintonExcel : ExcelBase, IExcelModel
    {
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public BadmintonExcel(string physicalTemplatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(physicalTemplatePath, setting, data)
        {
            this.ColumnTemplateCount = 11;
            this.ColumnStatus = 10;
            this.RowContentTemplate = 3;
        }

        /// <summary>
        /// Assign value to header section.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        protected override void AssignHeader(ISheet sheet)
        {
            if (sheet == null)
            {
                throw new ArgumentNullException("sheet");
            }

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
            row.Cells[colidx++].SetCellValue(LangResource.TotalPoints);
            row.Cells[colidx++].SetCellValue(LangResource.TotalSet);
            row.Cells[colidx].SetCellValue(LangResource.Status);
        }

        #region Assign Cell Data

        /// <summary>
        /// Set data for match.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        /// <param name="data">The view model.</param>
        /// <param name="currentRow">The current row.</param>
        protected override void SetDataForMatch(ISheet sheet, IResultViewModel data, ref int currentRow)
        {
            var exportRow = (BadmintonViewModel)data;

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
                exportRow.TotalPointHome,
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
                exportRow.TotalPointAway,
                exportRow.TotalSetAWay,
                exportRow.EventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}