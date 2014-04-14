// -----------------------------------------------------------------------
// <copyright file="BilliardExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// ---------------------------------------------------------------------

namespace Fanex.Result.Core.ExcelModels
{
    using System.Collections.Generic;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Utilities;
    using Fanex.Result.Core.ViewModels;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Billiard excel model class.
    /// </summary>
    public sealed class BilliardExcel : ExcelBase
    {
        #region Fields

        private IList<BilliardViewModel> _billiardData;

        #endregion Fields

        public BilliardExcel(string templatePath, string reporTitle, IList<BilliardViewModel> data, LangResources resource)
            : base(templatePath, reporTitle, resource)
        {
            this._billiardData = data;
            this.NumofRow = this._billiardData.Count;
            this.ColumnTemplateCount = 4;
            this.ColumnStatus = 3;
            this.RowContentTemplate = 3;
        }

        public override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(Lang.KickOffTime);
            row.Cells[colidx++].SetCellValue(Lang.Match);
            row.Cells[colidx++].SetCellValue(Lang.FinalScore);
            row.Cells[colidx].SetCellValue(Lang.Status);
        }

        public override void AssignSheetData(ISheet sheet)
        {
            var currentRow = 2;
            var currentLeagueId = 0;
            foreach (var exportRow in this._billiardData)
            {
                SetLeague(sheet, exportRow.LeagueId, exportRow.LeagueName, ref currentLeagueId, ref currentRow);
                SetMatch(sheet, exportRow, ref currentRow);
            }
        }

        #region Assign Cell Data

        /// <summary>
        /// Set value for match row.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        /// <param name="exportRow">The view model.</param>
        /// <param name="currentRow">The current row.</param>
        private void SetMatch(ISheet sheet, BilliardViewModel exportRow, ref int currentRow)
        {
            var getRowTeamWin = GetRowTeamWin(exportRow.FinalHomeScore, exportRow.FinalAwayScore);

            var rowHome = sheet.CopyRow(TemplateSheet, getRowTeamWin[0], currentRow);
            var rowAway = sheet.CopyRow(TemplateSheet, getRowTeamWin[1], currentRow + 1);

            MergeResultCells(sheet, currentRow, currentRow + 1, ColumnKickOffTime, this.ColumnStatus);

            AssignStringData(
                rowHome,
                             ColumnKickOffTime,
                           Helper.ToRtfString(exportRow.KickOffTime),
                             Helper.ToRtfString(exportRow.HomeTeam),
                             exportRow.FinalHomeScore,
                             exportRow.EventStatus);
            currentRow++;

            AssignStringData(
                rowAway,
                             ColumnKickOffTime,
                             Helper.ToRtfString(exportRow.KickOffTime),
                              Helper.ToRtfString(exportRow.AwayTeam),
                             exportRow.FinalAwayScore,
                             exportRow.EventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}