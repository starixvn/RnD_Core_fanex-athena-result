// -----------------------------------------------------------------------
// <copyright file="TennisExcel.cs" company="Nexcel">
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
    /// Tennis excel model class.
    /// </summary>
    internal sealed class TennisExcel : ExcelBase
    {
        #region Fields

        private IList<TennisViewModel> _tennisData;

        #endregion Fields

        public TennisExcel(string templatePath, string reporTitle, IList<TennisViewModel> data, LangResources resource)
            : base(templatePath, reporTitle, resource)
        {
            this._tennisData = data;
            this.NumofRow = this._tennisData.Count;
            this.ColumnTemplateCount = 10;
            this.ColumnStatus = 9;
            this.RowContentTemplate = 3;
        }

        /// <summary>
        /// Assign value to header section.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        public override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(Lang.KickOffTime);
            row.Cells[colidx++].SetCellValue(Lang.Match);
            row.Cells[colidx++].SetCellValue(Lang.Lbl1S);
            row.Cells[colidx++].SetCellValue(Lang.Lbl2S);
            row.Cells[colidx++].SetCellValue(Lang.Lbl3S);
            row.Cells[colidx++].SetCellValue(Lang.Lbl4S);
            row.Cells[colidx++].SetCellValue(Lang.Lbl5S);
            row.Cells[colidx++].SetCellValue(Lang.TotalGames);
            row.Cells[colidx++].SetCellValue(Lang.TotalSet);
            row.Cells[colidx].SetCellValue(Lang.Status);
        }

        public override void AssignSheetData(ISheet sheet)
        {
            var currentRow = 2;
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
        private void SetDataForMatch(ISheet sheet, TennisViewModel exportRow, ref int currentRow)
        {
            var getRowTeamWin = GetRowTeamWin(exportRow.TotalSetHome, exportRow.TotalSetAWay);

            var rowHome = sheet.CopyRow(TemplateSheet, getRowTeamWin[0], currentRow);

            var rowAway = sheet.CopyRow(TemplateSheet, getRowTeamWin[1], currentRow + 1);

            MergeResultCells(sheet, currentRow, currentRow + 1, ColumnKickOffTime, this.ColumnStatus);

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
                            exportRow.TotalGameHome,
                            exportRow.TotalSetHome,
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
                            exportRow.TotalGameAway,
                            exportRow.TotalSetAWay,
                             exportRow.EventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}