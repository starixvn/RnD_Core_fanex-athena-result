// -----------------------------------------------------------------------
// <copyright file="VSTennisExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ExcelModels
{
    using System.Collections.Generic;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.Utilities;
    using Fanex.Result.Core.ViewModels;
    using NPOI.SS.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal class VSTennisExcel : ExcelBase
    {
        #region Fields

        private IList<VSTennisViewModel> _tennisData;

        #endregion Fields

        public VSTennisExcel(string templatePath, ResultSetting setting, IList<VSTennisViewModel> data, LangResources resource)
            : base(templatePath, setting, resource)
        {
            this._tennisData = data;
            this.NumofRow = this._tennisData.Count;
            this.ColumnTemplateCount = 6 ;
            this.ColumnStatus = 4 ;
            this.RowContentTemplate = 3;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(Lang.MatchCode + "/" + Lang.ID);

            row.Cells[colidx++].SetCellValue(this.Lang.KickOffTime);
            row.Cells[colidx++].SetCellValue(this.Lang.Team);
            row.Cells[colidx++].SetCellValue(this.Lang.TotalPoints);
            row.Cells[colidx].SetCellValue(this.Lang.Status);
        }

        protected override void AssignSheetData(ISheet sheet)
        {
            var currentRow = RowLeagueTemplate;
            var currentLeagueId = 0;
            foreach (var exportRow in this._tennisData)
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
        private void SetDataForMatch(ISheet sheet, VSTennisViewModel exportRow, ref int currentRow)
        {
            var rowTeamWin = this.GetRowTeamWin(exportRow.HomePoint, exportRow.AwayPoint);
            var rowHome = sheet.CopyRow(TemplateSheet, rowTeamWin[0], currentRow);

            var rowAway = sheet.CopyRow(TemplateSheet, rowTeamWin[1], currentRow + 1);

            MergeResultCells(sheet, currentRow, currentRow + 1, ColumnKickOffTime, this.ColumnStatus);

            AssignStringData(
                            rowHome,
                             ColumnMatchCode,
                             exportRow.MatchCode,
                            Helper.ToRtfString(exportRow.KickOffTime),
                           Helper.ToRtfString(exportRow.HomeTeam),
                            exportRow.HomePoint,
                             exportRow.EventStatus);
            currentRow++;
            AssignStringData(
                        rowAway,
                             ColumnMatchCode,
                             exportRow.MatchId,
                               Helper.ToRtfString(exportRow.KickOffTime),
                           Helper.ToRtfString(exportRow.AwayTeam),
                            exportRow.AwayPoint,
                             exportRow.EventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}