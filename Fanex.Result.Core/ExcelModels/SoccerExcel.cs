// -----------------------------------------------------------------------
// <copyright file="SoccerExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// ---------------------------------------------------------------------

namespace Fanex.Result.Core.ExcelModels
{
    using System.Collections.Generic;
    using Fanex.Npoil.Extension;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.ViewModels;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Soccer excel model class.
    /// </summary>
    internal sealed class SoccerExcel : ExcelBase
    {
        #region Fields

        private IList<SoccerViewModel> _soccerData;

        #endregion Fields

        public SoccerExcel(string templatePath, string reporTitle, IList<SoccerViewModel> data, LangResources resource)
            : base(templatePath, reporTitle, resource)
        {
            this._soccerData = data;
            this.NumofRow = this._soccerData.Count;
            this.ColumnTemplateCount = 5;
            this.ColumnStatus = 4;
            this.RowContentTemplate = 3;
        }

        public override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(this.Lang.KickOffTime);
            row.Cells[colidx++].SetCellValue(this.Lang.Match);
            row.Cells[colidx++].SetCellValue(this.Lang.FirstHalfScore);
            row.Cells[colidx++].SetCellValue(this.Lang.FinalScore);
            row.Cells[colidx].SetCellValue(this.Lang.Status);
        }

        public override void AssignSheetData(ISheet sheet)
        {
            var currentRow = RowLeagueTemplate;
            var currentLeagueId = 0;
            foreach (var exportRow in this._soccerData)
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
        private void SetDataForMatch(ISheet sheet, SoccerViewModel exportRow, ref int currentRow)
        {
            var rowTeamWin = this.GetRowTeamWin(exportRow.FinalHomeScore, exportRow.FinalAwayScore);
            var rowHome = sheet.CopyRow(TemplateSheet, rowTeamWin[0], currentRow);

            var rowAway = sheet.CopyRow(TemplateSheet, rowTeamWin[1], currentRow + 1);

            MergeResultCells(sheet, currentRow, currentRow + 1, ColumnKickOffTime, this.ColumnStatus);

            AssignStringData(
                            rowHome,
                             ColumnKickOffTime,
                            this.ToRtfString(exportRow.KickOffTime),
                           this.ToRtfString(exportRow.HomeTeam),
                            exportRow.HTHomeScore,
                            exportRow.FinalHomeScore,
                             exportRow.EventStatus);
            currentRow++;
            AssignStringData(
                        rowAway,
                             ColumnKickOffTime,
                               this.ToRtfString(exportRow.KickOffTime),
                           this.ToRtfString(exportRow.AwayTeam),
                            exportRow.HTAwayScore,
                            exportRow.FinalAwayScore,
                             exportRow.EventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}