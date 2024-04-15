namespace Fanex.Result.Core.Sportsbook.ExcelModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.ExcelModels;
    using Base.Extension;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using NPOI.SS.UserModel;
    using ViewModels;

    internal sealed class SoccerExcel : ExcelBase, IExcelModel
    {
        private const string FirstGoal = "F";
        private const string LastGoal = "L";
        private const string FirstGoalHT = "1F";
        private const string LastGoalHT = "1L";
        private const int ColumnHTScore = 3;
        private const int ColumnFTScore = 4;

        public SoccerExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            ColumnTemplateCount = 6;
            ColumnStatus = 5;
            RowContentTemplate = 3;
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

        protected override void SetDataForMatch(ISheet sheet, IResultViewModel data, ref int currentRow)
        {
#pragma warning disable S3215 // "interface" instances should not be cast to concrete types
            var exportRow = (SoccerViewModel)data;
#pragma warning restore S3215 // "interface" instances should not be cast to concrete types

            var rowHome = sheet.CopyRow(TemplateSheet, GetRowTeamWin(exportRow.HomeWin), currentRow);
            var rowAway = sheet.CopyRow(TemplateSheet, GetRowTeamWin(exportRow.AwayWin), currentRow + 1);

            MergeResultCells(sheet, currentRow, currentRow + 1, ColumnKickOffTime, ColumnStatus);
            if(!string.IsNullOrEmpty(exportRow.HTLastSeriesChildMatchChoice))
            {
                sheet.MergeCells(currentRow, ColumnHTScore, currentRow + 1, ColumnHTScore);
            }

            if(!string.IsNullOrEmpty(exportRow.FTLastSeriesChildMatchChoice))
            {
                sheet.MergeCells(currentRow, ColumnFTScore, currentRow + 1, ColumnFTScore);
            }

            var eventStatus = exportRow.EventStatus;

            AssignStringData(
                rowHome,
                ColumnMatchCode,
                exportRow.MatchCode,
                Helper.ToRtfString(exportRow.KickOffTime),
                Helper.ToRtfString(CreateHomeTeamValue(exportRow)),
                CreateScoreValue(exportRow.HTHomeScore, exportRow.HTLastSeriesChildMatchChoice),
                CreateScoreValue(exportRow.FinalHomeScore, exportRow.FTLastSeriesChildMatchChoice),
                eventStatus);
            currentRow++;

            AssignStringData(
                rowAway,
                ColumnMatchCode,
                exportRow.MatchId,
                Helper.ToRtfString(exportRow.KickOffTime),
                Helper.ToRtfString(CreateAwayTeamValue(exportRow)),
                CreateScoreValue(exportRow.HTHomeScore, exportRow.HTLastSeriesChildMatchChoice),
                CreateScoreValue(exportRow.FinalHomeScore, exportRow.FTLastSeriesChildMatchChoice),
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

            awayTeam += " " + exportRow.PenaltyMatchDetail;

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

            homeTeam += " " + exportRow.PenaltyMatchDetail;

            return homeTeam;
        }

        private static string CreateScoreValue(string score, string lastSeriesChoice)
            => string.IsNullOrEmpty(lastSeriesChoice) ? score : lastSeriesChoice;

        #endregion Assign Cell Data
    }
}