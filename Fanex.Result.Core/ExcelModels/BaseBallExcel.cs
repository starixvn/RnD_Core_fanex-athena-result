// -----------------------------------------------------------------------
// <copyright file="BaseBallExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ExcelModels
{
    using System.Collections.Generic;
    using Fanex.Common.Extensions;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.ViewModels;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Baseball excel model class.
    /// </summary>
    internal sealed class BaseBallExcel : ExcelBase
    {
        #region Fields

        private IList<BaseBallViewModel> _baseBallData;

        #endregion Fields

        public BaseBallExcel(string templatePath, string reporTitle, IList<BaseBallViewModel> data, LangResources resource)
            : base(templatePath, reporTitle, resource)
        {
            this._baseBallData = data;
            this.NumofRow = this._baseBallData.Count;
            this.ColumnTemplateCount = 15;
            this.ColumnStatus = 14;
            this.RowContentTemplate = 3;
        }

        /// <summary>
        /// Assign data to header section.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        public override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(Lang.KickOffTime);
            row.Cells[colidx].SetCellValue(Lang.Event);

            colidx = 12;
            row.Cells[colidx++].SetCellValue(Lang.HTScore);
            row.Cells[colidx++].SetCellValue(Lang.FinalScore);
            row.Cells[colidx].SetCellValue(Lang.Status);
        }

        public override void AssignSheetData(ISheet sheet)
        {
            var currentRow = 2;
            var currentLeagueId = 0;
            foreach (var exportRow in this._baseBallData)
            {
                SetLeague(sheet, exportRow.LeagueId, exportRow.LeagueName, ref currentLeagueId, ref currentRow);
                AssignMatch(sheet, exportRow, ref currentRow);
            }
        }

        #region Assign Cell Data

        /// <summary>
        /// Assign value to match row.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        /// <param name="exportRow">The view model.</param>
        /// <param name="currentRow">The current row.</param>
        private void AssignMatch(ISheet sheet, BaseBallViewModel exportRow, ref int currentRow)
        {
            var getRowTeamWin = GetRowTeamWin(exportRow.FinalHomeScore, exportRow.FinalAwayScore);

            var rowHome = sheet.CopyRow(TemplateSheet, getRowTeamWin[0], currentRow);
            var rowAway = sheet.CopyRow(TemplateSheet, getRowTeamWin[1], currentRow + 1);

            MergeResultCells(sheet, currentRow, currentRow + 1, ColumnKickOffTime, ColumnStatus);
            AssignStringData(
                            rowHome,
                             ColumnKickOffTime,
                             this.ToRtfString(exportRow.KickOffTime),
                            this.ToRtfString(exportRow.HomeTeam),
                             exportRow.GetHomeAwaySet(0, true),
                            exportRow.GetHomeAwaySet(1, true),
                            exportRow.GetHomeAwaySet(2, true),
                            exportRow.GetHomeAwaySet(3, true),
                            exportRow.GetHomeAwaySet(4, true),
                            exportRow.GetHomeAwaySet(5, true),
                            exportRow.GetHomeAwaySet(6, true),
                            exportRow.GetHomeAwaySet(7, true),
                             exportRow.GetHomeAwaySet(8, true),
                             exportRow.EIHome,
                             exportRow.HTHomeScore,
                             exportRow.FinalHomeScore,
                             exportRow.EventStatus);
            currentRow++;

            AssignStringData(
                rowAway,
                ColumnKickOffTime,
                             this.ToRtfString(exportRow.KickOffTime),
                            this.ToRtfString(exportRow.AwayTeam),
                             exportRow.GetHomeAwaySet(0, false),
                            exportRow.GetHomeAwaySet(1, false),
                            exportRow.GetHomeAwaySet(2, false),
                            exportRow.GetHomeAwaySet(3, false),
                            exportRow.GetHomeAwaySet(4, false),
                            exportRow.GetHomeAwaySet(5, false),
                            exportRow.GetHomeAwaySet(6, false),
                            exportRow.GetHomeAwaySet(7, false),
                             exportRow.GetHomeAwaySet(8, false),
                             exportRow.EIAway,
                             exportRow.HTAwayScore,
                             exportRow.FinalAwayScore,
                             exportRow.EventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}