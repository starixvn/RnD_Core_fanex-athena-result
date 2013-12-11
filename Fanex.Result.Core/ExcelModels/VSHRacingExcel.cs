// -----------------------------------------------------------------------
// <copyright file="VSHRacingExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.ExcelModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Common.Extensions;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.ViewModels;
    using NPOI.SS.UserModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public sealed class VSHRacingExcel : ExcelBase
    {
        #region Fields

        private const string FormatStatus = "Offical {0} {1}";

        private IList<VSHRacingViewModel> _racingData;

        #endregion Fields

        public VSHRacingExcel(string templatePath, string reporTitle, IList<VSHRacingViewModel> data, LangResources resource)
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
        /// <param name="currentLeagueId">The current league id.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="endRowSpan">The end row span.</param>
        private void SetMatch(ISheet sheet, VSHRacingViewModel exportRow, ref int currentLeagueId, ref int currentRow, ref int endRowSpan)
        {
            var cellIndex = 0;

            var leagueId = exportRow.LeagueId;
            var currentRace = DateTime.MinValue;
            SetLeague(sheet, exportRow.LeagueId, exportRow.LeagueName, ref currentLeagueId, ref currentRow);

            if (currentLeagueId == 0 || currentLeagueId != leagueId ||
                currentRace != exportRow.EvenTime)
            {
                currentRace = exportRow.EvenTime;

                // Calculate row span
                var rowsTopRace =
                this._racingData.Count(i => i.LeagueId == leagueId && i.EvenTime == currentRace && i.Place < 5);

                if (currentRow == 3 || endRowSpan < currentRow)
                {
                    endRowSpan = currentRow + rowsTopRace - 1;
                    MergeKickOffTimeCell(sheet, currentRow, endRowSpan, colSpanKichOffTime: ColumnIndex);
                }
            }

            var rowHome = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow);
            this.AssignStringData(
                rowHome,
                cellIndex,
                this.ToRtfString(exportRow.KickOffTime),
                exportRow.Placing,
                exportRow.HorseNo,
                exportRow.Result,
                exportRow.WinOdd,
                exportRow.PlaceOdd,
                exportRow.Status);

            currentRow++;
        }

        private void MergeKickOffTimeCell(ISheet sheet, int currentRow, int rowSpan, int colSpanKichOffTime)
        {
            sheet.MergeCells(currentRow, colSpanKichOffTime, rowSpan, colSpanKichOffTime);
        }

        #endregion Assign Cell Data
    }
}