// -----------------------------------------------------------------------
// <copyright file="LiveCasinoExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// ---------------------------------------------------------------------

namespace Fanex.Result.Core.ExcelModels
{
    using System.Collections.Generic;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Utilities;
    using Fanex.Result.Core.ViewModels;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Live casino excel model class.
    /// </summary>
    internal sealed class LiveCasinoExcel : ExcelBase
    {
        private IList<LiveCasinoViewModel> _liveCasinoData;

        public LiveCasinoExcel(
            string templatePath,
            string reporTitle,
            IList<LiveCasinoViewModel> data,
            LangResources resource)
            : base(templatePath, reporTitle, resource)
        {
            this._liveCasinoData = data;
            this.NumofRow = this._liveCasinoData.Count;
            this.ColumnTemplateCount = 5;
            this.ColumnStatus = 4;
            this.RowContentTemplate = 3;
        }

        public override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(this.Lang.KickOffTime);
            row.Cells[colidx++].SetCellValue(this.Lang.GameTypeAndTableNumber);
            row.Cells[colidx++].SetCellValue(this.Lang.FinalScore);
            row.Cells[colidx++].SetCellValue(this.Lang.Results);
            row.Cells[colidx].SetCellValue(this.Lang.Status);
        }

        /// <summary>
        /// Assign value to sheet.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        public override void AssignSheetData(ISheet sheet)
        {
            var currentRow = RowLeagueTemplate;
            var currentLeagueId = 0;
            foreach (var exportRow in this._liveCasinoData)
            {
                if (currentLeagueId == 0 || currentLeagueId != exportRow.LeagueId)
                {
                    currentLeagueId = exportRow.LeagueId;
                    var rowLeague = sheet.CopyRow(TemplateSheet, RowLeagueTemplate, currentRow, ColumnTemplateIndexList);
                    rowLeague.Cells[0].SetCellValue(exportRow.LeagueName);
                    sheet.MergeCells(currentRow, 0, currentRow, 1);
                    currentRow++;
                    if (string.IsNullOrEmpty(exportRow.PlayerAndBanker))
                    {
                        rowLeague.Cells[2].SetCellValue(string.Empty);
                    }
                }

                var rowMatch = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow, ColumnTemplateIndexList);

                currentRow++;

                AssignStringData(
                    rowMatch,
                    ColumnKickOffTime,
                    Helper.ToRtfString(exportRow.KickOffTime),
                    exportRow.GameType,
                    exportRow.FinalScore,
                    Helper.ToRtfString(exportRow.Result),
                    exportRow.EventStatus);
            }
        }

        protected override IList<int> GetColumnTemplateIndex()
        {
            return new List<int> { 0, 1, 2, 3, 4 };
        }
    }
}