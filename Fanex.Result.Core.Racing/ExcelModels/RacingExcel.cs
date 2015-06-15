// -----------------------------------------------------------------------
// <copyright file="RacingExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// ---------------------------------------------------------------------

namespace Fanex.Result.Core.Racing.ExcelModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.DataDomain;
    using Fanex.Result.Core.Base.ExcelModels;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Racing.ViewModels;
    using NPOI.SS.UserModel;
    /// <summary>
    /// Racing excel model class.
    /// </summary>
    internal sealed class RacingExcel : ExcelBase, IExcelModel
    {
        private string _formatStatus = string.Empty;

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public RacingExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data, LangResources resource)
            : base(templatePath, setting, data, resource)
        {
            this.ColumnTemplateCount = 7;
            this.ColumnStatus = 6;
            this.RowContentTemplate = 3;
            this._formatStatus = resource.Official + " {0} {1}";
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 3;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[ColumnRace].SetCellValue(Lang.Race);
            row.Cells[colidx++].SetCellValue(Lang.Results);
            row.Cells[colidx++].SetCellValue(Lang.Win);
            row.Cells[colidx++].SetCellValue(Lang.Place);
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
            var endRowSpan = 3;
            var groupData = this.ViewModelData.GroupBy(i => i.LeagueId);
            foreach (var item in groupData)
            {
                foreach (RacingViewModel exportRow in item)
                {
                    if (exportRow.Place > 4)
                    {
                        continue;
                    }

                    SetMatch(sheet, exportRow, ref currentLeagueId, ref currentRow, ref endRowSpan);
                }
            }
        }

        protected override void ProcessUI(ISheet sheet)
        {
            /*
             * Void processing UI
             */
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
                var rowsTopRace = this.ViewModelData.Where(i => i.LeagueId == leagueId)
                    .Select(i => (RacingViewModel)i)
                    .Count(i => i.RaceName.Equals(currentRace) && i.Place < 5);

                if (currentRow == 3 || endRowSpan < currentRow)
                {
                    endRowSpan = currentRow + rowsTopRace - 1;
                    MergeResultCells(sheet, currentRow, endRowSpan, ColumnRace, this.ColumnStatus);
                }

                status = string.Format(
                    this._formatStatus,
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