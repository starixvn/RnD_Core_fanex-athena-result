// -----------------------------------------------------------------------
// <copyright file="HockeyExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// ---------------------------------------------------------------------

namespace Fanex.Result.Core.ExcelModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.Utilities;
    using Fanex.Result.Core.ViewModels;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Hockey excel model class.
    /// </summary>
    internal sealed class HockeyExcel : ExcelBase, IExcelModel
    {
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public HockeyExcel(
            string templatePath,
            ResultSetting setting,
            IList<IResultViewModel> data,
            LangResources resource)
            : base(templatePath, setting, data, resource)
        {
            this.ColumnTemplateCount = 9;
            this.ColumnStatus = 8;
            this.RowContentTemplate = 3;
        }

        /// <summary>
        /// Assign value to header section.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(Lang.MatchCode + "/" + Lang.ID);

            row.Cells[colidx++].SetCellValue(Lang.KickOffTime);
            row.Cells[colidx++].SetCellValue(Lang.Match);
            row.Cells[colidx++].SetCellValue(Lang.Lbl1P);
            row.Cells[colidx++].SetCellValue(Lang.Lbl2P);
            row.Cells[colidx++].SetCellValue(Lang.Lbl3P);
            row.Cells[colidx++].SetCellValue(Lang.LblOT);
            row.Cells[colidx++].SetCellValue(Lang.FinalScore);
            row.Cells[colidx].SetCellValue(Lang.Status);
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
            var exportRow = (HockeyViewModel)data;

            var getRowTeamWin = GetRowTeamWin(exportRow.FinalHomeScore, exportRow.FinalAwayScore);

            var rowHome = sheet.CopyRow(TemplateSheet, getRowTeamWin[0], currentRow);

            var rowAway = sheet.CopyRow(TemplateSheet, getRowTeamWin[1], currentRow + 1);

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
                             exportRow.OTHome,
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
                             exportRow.OTAway,
                             exportRow.FinalAwayScore,
                            exportRow.EventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}