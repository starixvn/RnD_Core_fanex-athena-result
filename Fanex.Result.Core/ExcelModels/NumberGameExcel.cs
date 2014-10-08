// -----------------------------------------------------------------------
// <copyright file="NumberGameExcel.cs" company="Nexcel">
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

    /// <summary>
    /// Number game excel model class.
    /// </summary>
    internal sealed class NumberGameExcel : ExcelBase
    {
        #region Fields

        private const string Match = "{0} {1}";

        private IList<NumberGameViewModel> _numberGameData;

        #endregion Fields

        public NumberGameExcel(string templatePath, ResultSetting setting, IList<NumberGameViewModel> data, LangResources resource)
            : base(templatePath, setting, resource)
        {
            this._numberGameData = data;
            this.NumofRow = this._numberGameData.Count;
            this.ColumnTemplateCount = 9;
            this.ColumnStatus = 8;
            this.RowContentTemplate = 3;
        }

        /// <summary>
        /// Assign value to header section.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        protected override void AssignHeader(NPOI.SS.UserModel.ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(Lang.MatchCode + "/" + Lang.ID);
            row.Cells[colidx++].SetCellValue(Lang.KickOffTime);
            row.Cells[colidx++].SetCellValue(Lang.Match);
            row.Cells[colidx++].SetCellValue(Lang.Results);
            row.Cells[colidx++].SetCellValue(Lang.Warrior);
            row.Cells[colidx++].SetCellValue(Lang.LblOEFT);
            row.Cells[colidx++].SetCellValue(Lang.Lbl1stBall);
            row.Cells[colidx++].SetCellValue(Lang.LastBall);
            row.Cells[colidx].SetCellValue(Lang.Status);
        }

        /// <summary>
        /// Assign data to sheet.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        protected override void AssignSheetData(NPOI.SS.UserModel.ISheet sheet)
        {
            var currentRow = RowLeagueTemplate;
            var currentLeagueId = 0;

            foreach (var exportRow in this._numberGameData)
            {
                if (currentLeagueId == 0 || currentLeagueId != exportRow.LeagueId)
                {
                    SetLeague(sheet, exportRow.LeagueId, exportRow.LeagueName, ref currentLeagueId, ref currentRow);
                }

                var rowMatch = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow);

                var results = new List<string>();

                foreach (var ball in exportRow.Result)
                {
                    results.Add(ball);
                }

                AssignStringData(
                            rowMatch,
                                ColumnMatchCode,
                                Helper.ToRtfString(exportRow.MatchCode + "<br/>" + exportRow.MatchId),
                                 Helper.ToRtfString(exportRow.KickOffTime),
                                  string.Format(Match, Lang.NumberGameNo, exportRow.GameNo),
                                  string.Join("-", results),
                                  exportRow.Warrior,
                                  exportRow.OEFT,
                                  exportRow.FirstBall,
                                  exportRow.LastBall,
                                   exportRow.EventStatus);
                currentRow++;
            }
        }
    }
}