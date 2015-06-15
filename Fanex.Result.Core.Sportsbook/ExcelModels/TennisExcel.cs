// -----------------------------------------------------------------------
// <copyright file="TennisExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// ---------------------------------------------------------------------

namespace Fanex.Result.Core.Sportsbook.ExcelModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.ExcelModels;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Sportsbook.ViewModels;
    using NPOI.SS.UserModel;
    using Fanex.Result.Core.Base.DataDomain;
    /// <summary>
    /// Tennis excel model class.
    /// </summary>
    internal sealed class TennisExcel : ExcelBase, IExcelModel
    {
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public TennisExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data, LangResources resource)
            : base(templatePath, setting, data, resource)
        {
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
            row.Cells[colidx++].SetCellValue(Lang.Lbl1S);
            row.Cells[colidx++].SetCellValue(Lang.Lbl2S);
            row.Cells[colidx++].SetCellValue(Lang.Lbl3S);
            row.Cells[colidx++].SetCellValue(Lang.Lbl4S);
            row.Cells[colidx++].SetCellValue(Lang.Lbl5S);
            row.Cells[colidx++].SetCellValue(Lang.TotalGames);
            row.Cells[colidx++].SetCellValue(Lang.TotalSet);
            row.Cells[colidx].SetCellValue(Lang.Status);
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
            var exportRow = (TennisViewModel)data;
            var rowHome = sheet.CopyRow(TemplateSheet, this.GetRowTeamWin(exportRow.HomeWin), currentRow);

            var rowAway = sheet.CopyRow(TemplateSheet, this.GetRowTeamWin(exportRow.AwayWin), currentRow + 1);

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
                                  exportRow.GetHomeAwaySet(3, true),
                                    exportRow.GetHomeAwaySet(4, true),
                            exportRow.TotalGameHome,
                            exportRow.TotalSetHome,
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