// -----------------------------------------------------------------------
// <copyright file="SoccerExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// ---------------------------------------------------------------------
namespace Fanex.Result.Core.Sportsbook.ExcelModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.ExcelModels;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Sportsbook.ViewModels;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Soccer excel model class.
    /// </summary>
    internal sealed class SoccerExcel : ExcelBase, IExcelModel
    {
        private const string FirstGoal = "F";
        private const string LastGoal = "L";
        private const string FirstGoalHT = "1F";
        private const string LastGoalHT = "1L";

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public SoccerExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            this.ColumnTemplateCount = 6;
            this.ColumnStatus = 5;
            this.RowContentTemplate = 3;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(LangResource.MatchCode + "/" + LangResource.ID);
            row.Cells[colidx++].SetCellValue(LangResource.KickOffTime);
            row.Cells[colidx++].SetCellValue(LangResource.Match);
            row.Cells[colidx++].SetCellValue(LangResource.HTScore);
            row.Cells[colidx++].SetCellValue(LangResource.FinalScore);
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
            var exportRow = (SoccerViewModel)data;
            var rowHome = sheet.CopyRow(TemplateSheet, this.GetRowTeamWin(exportRow.HomeWin), currentRow);

            var rowAway = sheet.CopyRow(TemplateSheet, this.GetRowTeamWin(exportRow.AwayWin), currentRow + 1);

            MergeResultCells(sheet, currentRow, currentRow + 1, ColumnKickOffTime, this.ColumnStatus);
            var eventStatus = exportRow.EventStatus;

            AssignStringData(
                            rowHome,
                             ColumnMatchCode,
                             exportRow.MatchCode,
                            Helper.ToRtfString(exportRow.KickOffTime),
                           Helper.ToRtfString(CreateHomeTeamValue(exportRow)),
                            exportRow.HTHomeScore,
                            exportRow.FinalHomeScore,
                             eventStatus);
            currentRow++;

            AssignStringData(
                        rowAway,
                             ColumnMatchCode,
                             exportRow.MatchId,
                               Helper.ToRtfString(exportRow.KickOffTime),
                           Helper.ToRtfString(CreateAwayTeamValue(exportRow)),
                            exportRow.HTAwayScore,
                            exportRow.FinalAwayScore,
                             eventStatus);
            currentRow++;
        }

        /// <summary>
        /// Create away team with first goal and last goal status.
        /// </summary>
        /// <param name="exportRow">The row data.</param>
        /// <returns>The away value.</returns>
        private static string CreateAwayTeamValue(SoccerViewModel exportRow)
        {
            var awayFG = new string[4];
            if (exportRow.IsShowedFirstHalfFGAway)
            {
                awayFG[0] = FirstGoalHT;
            }

            if (exportRow.IsShowedFirstHalfLGAway)
            {
                awayFG[1] = LastGoalHT;
            }

            if (exportRow.IsShowFGAway)
            {
                awayFG[2] = FirstGoal;
            }

            if (exportRow.IsShowLGAway)
            {
                awayFG[3] = LastGoal;
            }

            var realAwayFG = awayFG.Where(i => !string.IsNullOrEmpty(i)).ToList();
            var awayTeam = exportRow.AwayTeam;
            if (realAwayFG.Count > 0)
            {
                awayTeam += " (" + string.Join(",", realAwayFG) + ")";
            }

            return awayTeam;
        }

        /// <summary>
        /// Create home team with first goal and last goal status.
        /// </summary>
        /// <param name="exportRow">The row data.</param>
        /// <returns>The home value.</returns>
        private static string CreateHomeTeamValue(SoccerViewModel exportRow)
        {
            var homeFG = new string[4];
            if (exportRow.IsShowedFirstHalfFGHome)
            {
                homeFG[0] = FirstGoalHT;
            }

            if (exportRow.IsShowedFirstHalfLGHome)
            {
                homeFG[1] = LastGoalHT;
            }

            if (exportRow.IsShowFGHome)
            {
                homeFG[2] = FirstGoal;
            }

            if (exportRow.IsShowLGHome)
            {
                homeFG[3] = LastGoal;
            }

            var realHomeFG = homeFG.Where(i => !string.IsNullOrEmpty(i)).ToList();
            var homeTeam = exportRow.HomeTeam;
            if (realHomeFG.Count > 0)
            {
                homeTeam += " (" + string.Join(",", realHomeFG) + ")";
            }

            return homeTeam;
        }

        #endregion Assign Cell Data
    }
}