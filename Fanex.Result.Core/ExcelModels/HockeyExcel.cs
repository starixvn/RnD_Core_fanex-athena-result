// -----------------------------------------------------------------------
// <copyright file="HockeyExcel.cs" company="Nexcel">
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
    /// Hockey excel model class.
    /// </summary>
    internal sealed class HockeyExcel : ExcelBase, IExcelModel
    {
        #region Fields

        private IList<HockeyViewModel> _hockeyData;

        #endregion Fields

        public HockeyExcel(
            string templatePath,
            ResultSetting setting,
            IList<HockeyViewModel> data,
            LangResources resource)
            : base(templatePath, setting, resource)
        {
            this._hockeyData = data;
            this.NumofRow = this._hockeyData.Count;
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

        /// <summary>
        /// Assign data to sheet.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        protected override void AssignSheetData(ISheet sheet)
        {
            var currentRow = 2;
            var currentLeagueId = 0;
            var groupData = this._hockeyData.GroupBy(i => i.LeagueId);
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
        private void SetDataForMatch(ISheet sheet, HockeyViewModel exportRow, ref int currentRow)
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