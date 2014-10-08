// -----------------------------------------------------------------------
// <copyright file="BasketballExcel.cs" company="Nexcel">
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
    /// Basketball excel model class.
    /// </summary>
    internal sealed class BasketballExcel : ExcelBase
    {
        #region Fields

        private IList<BasketBallViewModel> _basketBalllData;

        #endregion Fields

        public BasketballExcel(string templatePath, ResultSetting setting, IList<BasketBallViewModel> data, LangResources resource)
            : base(templatePath, setting, resource)
        {
            this._basketBalllData = data;
            this.NumofRow = this._basketBalllData.Count;
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
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(Lang.MatchCode + "/" + Lang.ID);
            row.Cells[colidx++].SetCellValue(Lang.KickOffTime);
            row.Cells[colidx++].SetCellValue(Lang.Match);
            row.Cells[colidx++].SetCellValue(Lang.Lbl1Q);
            row.Cells[colidx++].SetCellValue(Lang.Lbl2Q);
            row.Cells[colidx++].SetCellValue(Lang.Lbl3Q);
            row.Cells[colidx++].SetCellValue(Lang.Lbl4Q);
            row.Cells[colidx++].SetCellValue(Lang.LblOT);
            row.Cells[colidx++].SetCellValue(Lang.FirstHalfScore);
            row.Cells[colidx++].SetCellValue(Lang.FinalScore);
            row.Cells[colidx].SetCellValue(Lang.Status);
        }

        protected override void AssignSheetData(ISheet sheet)
        {
            var currentRow = 2;
            var currentLeagueId = 0;
            foreach (var exportRow in this._basketBalllData)
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
        private void SetDataForMatch(ISheet sheet, BasketBallViewModel exportRow, ref int currentRow)
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
                           exportRow.GetQHomeAway(0, true),
                            exportRow.GetQHomeAway(1, true),
                             exportRow.GetQHomeAway(2, true),
                              exportRow.GetQHomeAway(3, true),
                            exportRow.QTHome,
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
                           exportRow.GetQHomeAway(0, false),
                            exportRow.GetQHomeAway(1, false),
                             exportRow.GetQHomeAway(2, false),
                              exportRow.GetQHomeAway(3, false),
                            exportRow.QTAway,
                            exportRow.HTAwayScore,
                             exportRow.FinalAwayScore,
                            exportRow.EventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}