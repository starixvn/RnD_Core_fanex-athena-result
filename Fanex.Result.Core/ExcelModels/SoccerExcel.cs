// -----------------------------------------------------------------------
// <copyright file="SoccerExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// ---------------------------------------------------------------------
namespace Fanex.Result.Core.ExcelModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.Utilities;
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

        public SoccerExcel(string templatePath, ResultSetting setting, IList<SoccerViewModel> data, LangResources resource)
            : base(templatePath, setting, resource)
        {
            this._soccerData = data;
            this.NumofRow = this._soccerData.Count;
            this.ColumnTemplateCount = 6;
            this.ColumnStatus = 5;
            this.RowContentTemplate = 3;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(Lang.MatchCode + "/" + Lang.ID);
            row.Cells[colidx++].SetCellValue(this.Lang.KickOffTime);
            row.Cells[colidx++].SetCellValue(this.Lang.Match);
            row.Cells[colidx++].SetCellValue(this.Lang.FirstHalfScore);
            row.Cells[colidx++].SetCellValue(this.Lang.FinalScore);
            row.Cells[colidx].SetCellValue(this.Lang.Status);
        }

        /// <summary>
        /// Assign data to sheet.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        protected override void AssignSheetData(ISheet sheet)
        {
            var currentRow = RowLeagueTemplate;
            var currentLeagueId = 0;
            var groupData = this._soccerData.GroupBy(i => i.LeagueId);
            foreach (var item in groupData)
            {
                var firstItem = item.FirstOrDefault();

                SetLeague(sheet, firstItem.LeagueId, firstItem.LeagueName, ref currentLeagueId, ref currentRow);
                foreach (var exportRow in item)
                {
                    SetDataForMatch(sheet, exportRow, ref currentRow);
                }
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
            var eventStatus = exportRow.EventStatus;
            if (!string.IsNullOrEmpty(exportRow.PenaltyShootoutResult))
            {
                eventStatus = Helper.ToRtfString(exportRow.EventStatus + "<br />[" + Lang.PS + ":" + exportRow.PenaltyShootoutResult + "]");
            }

            AssignStringData(
                            rowHome,
                             ColumnMatchCode,
                             exportRow.MatchCode,
                            Helper.ToRtfString(exportRow.KickOffTime),
                           Helper.ToRtfString(exportRow.HomeTeam),
                            exportRow.HTHomeScore,
                            exportRow.FinalHomeScore,
                             eventStatus);
            currentRow++;
            AssignStringData(
                        rowAway,
                             ColumnMatchCode,
                             exportRow.MatchId,
                               Helper.ToRtfString(exportRow.KickOffTime),
                           Helper.ToRtfString(exportRow.AwayTeam),
                            exportRow.HTAwayScore,
                            exportRow.FinalAwayScore,
                             eventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}