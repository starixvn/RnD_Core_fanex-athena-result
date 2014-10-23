// -----------------------------------------------------------------------
// <copyright file="BilliardExcel.cs" company="Nexcel">
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
    /// Billiard excel model class.
    /// </summary>
    internal sealed class BilliardExcel : ExcelBase, IExcelModel
    {
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public BilliardExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data, LangResources resource)
            : base(templatePath, setting, data, resource)
        {
            this.ColumnTemplateCount = 5;
            this.ColumnStatus = 4;
            this.RowContentTemplate = 3;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(Lang.MatchCode + "/" + Lang.ID);

            row.Cells[colidx++].SetCellValue(Lang.KickOffTime);
            row.Cells[colidx++].SetCellValue(Lang.Match);
            row.Cells[colidx++].SetCellValue(Lang.FinalScore);
            row.Cells[colidx].SetCellValue(Lang.Status);
        }

        #region Assign Cell Data

        /// <summary>
        /// Set value for match row.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        /// <param name="data">The view model.</param>
        /// <param name="currentRow">The current row.</param>
        protected override void SetDataForMatch(ISheet sheet, IResultViewModel data, ref int currentRow)
        {
            var exportRow = (BilliardViewModel)data;
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
                             exportRow.FinalHomeScore,
                             exportRow.EventStatus);
            currentRow++;

            AssignStringData(
                rowAway,
                             ColumnMatchCode,
                             exportRow.MatchId,
                             Helper.ToRtfString(exportRow.KickOffTime),
                              Helper.ToRtfString(exportRow.AwayTeam),
                             exportRow.FinalAwayScore,
                             exportRow.EventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}