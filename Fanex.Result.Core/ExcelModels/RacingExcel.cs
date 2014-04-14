// -----------------------------------------------------------------------
// <copyright file="RacingExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// ---------------------------------------------------------------------

namespace Fanex.Result.Core.ExcelModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.ViewModels;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Racing excel model class.
    /// </summary>
    internal sealed class RacingExcel : ExcelBase
    {
        #region Fields

        private const string FormatStatus = "Offical {0} {1}";

        private IList<RacingViewModel> _racingData;

        #endregion Fields

        public RacingExcel(string templatePath, string reporTitle, IList<RacingViewModel> data, LangResources resource)
            : base(templatePath, reporTitle, resource)
        {
            this._racingData = data;
            this.NumofRow = this._racingData.Count;
            this.ColumnTemplateCount = 7;
            this.ColumnStatus = 6;
            this.RowContentTemplate = 3;
        }

        public override void AssignHeader(ISheet sheet)
        {
            var colidx = 3;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[0].SetCellValue(Lang.Race);
            row.Cells[colidx++].SetCellValue(Lang.Results);
            row.Cells[colidx++].SetCellValue(Lang.Win);
            row.Cells[colidx++].SetCellValue(Lang.Place);
            row.Cells[colidx].SetCellValue(Lang.Status);
        }

        /// <summary>
        /// Assign data to sheet.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        public override void AssignSheetData(ISheet sheet)
        {
            var currentRow = 2;
            var currentLeagueId = 0;
            var endRowSpan = 3;
            foreach (var exportRow in this._racingData)
            {
                if (exportRow.Place > 4)
                {
                    continue;
                }

                SetMatch(sheet, exportRow, ref currentLeagueId, ref currentRow, ref endRowSpan);
            }
        }

        #region Assign Cell Data

        /// <summary>
        /// Set data for match row.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        /// <param name="exportRow">The view model.</param>
        /// <param name="currentLeagueId">The league id.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="endRowSpan">The end row span.</param>
        private void SetMatch(ISheet sheet, RacingViewModel exportRow, ref int currentLeagueId, ref int currentRow, ref int endRowSpan)
        {
            var cellIndex = 0;
            var status = string.Empty;
            var leagueId = exportRow.LeagueId;
            var currentRace = string.Empty;
            SetLeague(sheet, exportRow.LeagueId, exportRow.LeagueName, ref currentLeagueId, ref currentRow);
            if (currentLeagueId == 0 || currentLeagueId != leagueId ||
                !currentRace.Equals(exportRow.RaceName))
            {
                currentRace = exportRow.RaceName;

                // Calculate row span
                var rowsTopRace =
                this._racingData.Count(i => i.LeagueId == leagueId && i.RaceName.Equals(currentRace) && i.Place < 5);

                if (currentRow == 3 || endRowSpan < currentRow)
                {
                    endRowSpan = currentRow + rowsTopRace - 1;
                    MergeResultCells(sheet, currentRow, endRowSpan, ColumnIndex, this.ColumnStatus);
                }

                status = string.Format(
                    FormatStatus,
                    string.IsNullOrEmpty(exportRow.SCR) ? string.Empty : "\n" + Lang.SCR + ":" + exportRow.SCR,
                    string.IsNullOrEmpty(exportRow.TF) ? string.Empty : "\n" + Lang.TF + ":" + exportRow.TF);
            }

            var rowHome = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow);

            this.AssignStringData(
                rowHome,
                cellIndex,
                currentRace,
                exportRow.Placing,
                exportRow.HorseNo,
                exportRow.Result,
                exportRow.WinOdd,
                exportRow.PlaceOdd,
                status);

            currentRow++;
        }

        #endregion Assign Cell Data
    }
}