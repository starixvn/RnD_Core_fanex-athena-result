// -----------------------------------------------------------------------
// <copyright file="OtherSportExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// ---------------------------------------------------------------------

namespace Fanex.Result.Core.ExcelModels
{
    using System.Collections.Generic;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.Utilities;
    using Fanex.Result.Core.ViewModels;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Other sports excel model class.
    /// </summary>
    internal sealed class OtherSportExcel : ExcelBase
    {
        #region Fields

        private IList<OtherViewModel> _otherSportData;

        #endregion Fields

        public OtherSportExcel(string templatePath, ResultSetting setting, IList<OtherViewModel> data, LangResources resource)
            : base(templatePath, setting, resource)
        {
            this._otherSportData = data;
            this.NumofRow = this._otherSportData.Count;
            this.ColumnTemplateCount = 6;
            this.ColumnStatus = 5;
            this.RowContentTemplate = 3;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(Lang.MatchCode + "/" + Lang.ID);
            row.Cells[colidx++].SetCellValue(Lang.KickOffTime);
            row.Cells[colidx++].SetCellValue(Lang.Match);
            row.Cells[colidx++].SetCellValue(Lang.FirstHalfScore);
            row.Cells[colidx++].SetCellValue(Lang.FinalScore);
            row.Cells[colidx].SetCellValue(Lang.Status);
        }

        protected override void AssignSheetData(ISheet sheet)
        {
            var currentRow = 2;
            var currentLeagueId = 0;
            foreach (var exportRow in this._otherSportData)
            {
                SetLeague(sheet, exportRow.LeagueId, exportRow.LeagueName, ref currentLeagueId, ref currentRow);
                SetDataForMatch(sheet, exportRow, ref currentRow);
            }
        }

        #region Assign Cell Data

        /// <summary>
        /// Set data for match.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        /// <param name="exportRow">The view model.</param>
        /// <param name="currentRow">The current row.</param>
        private void SetDataForMatch(ISheet sheet, OtherViewModel exportRow, ref int currentRow)
        {
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
                          exportRow.HTHomeScore,
                       exportRow.FinalHomeScore,
                             exportRow.EventStatus);
            currentRow++;
            AssignStringData(
                rowAway,
                             ColumnMatchCode,
                             exportRow.MatchId,
                             Helper.ToRtfString(exportRow.KickOffTime),
                              Helper.ToRtfString(exportRow.AwayTeam),
                          exportRow.HTAwayScore,
                       exportRow.FinalAwayScore,
                             exportRow.EventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}