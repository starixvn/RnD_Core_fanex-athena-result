// -----------------------------------------------------------------------
// <copyright file="OutRightExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// ---------------------------------------------------------------------

namespace Fanex.Result.Core.ExcelModels
{
    using System.Collections.Generic;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.ViewModels;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Outright excel model class.
    /// </summary>
    internal sealed class OutRightExcel : ExcelBase
    {
        #region Fields

        private const int ColumnTime = 0;
        private const int ColumnTeam = 1;
        private const int ColumnResults = 2;
        private const int RowNoInfo = 4;

        private IList<OutRightViewModel> _outRightdata;

        #endregion Fields

        public OutRightExcel(string templatePath, string reporTitle, IList<OutRightViewModel> data, LangResources resource)
            : base(templatePath, reporTitle, resource)
        {
            this._outRightdata = data;
            this.NumofRow = this._outRightdata.Count;
            this.ColumnTemplateCount = 3;
            this.ColumnStatus = 4;
            this.RowContentTemplate = 3;
        }

        public override void AssignHeader(ISheet sheet)
        {
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[ColumnTime].SetCellValue(Lang.Time);
            row.Cells[ColumnTeam].SetCellValue(Lang.Team);
            row.Cells[ColumnResults].SetCellValue(Lang.Results);
        }

        public override void AssignSheetData(ISheet sheet)
        {
            var currentRow = 2;
            var currentLeagueId = 0;
            foreach (var exportRow in this._outRightdata)
            {
                SetLeague(sheet, exportRow.LeagueId, exportRow.LeagueName, ref currentLeagueId, ref currentRow);
                SetDataForMatch(sheet, exportRow, ref currentRow);
            }
        }

        #region Assign Cell Data

        public override void AssignNoInfoRow(ISheet sheet)
        {
            SetTextRowValue(sheet, RowNoInfo, RowLeagueTemplate, ColumnTemplateCount, Lang.NoInfo);
        }

        /// <summary>
        /// Set data for match.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        /// <param name="exportRow">The view model.</param>
        /// <param name="currentRow">The current row.</param>
        private void SetDataForMatch(ISheet sheet, OutRightViewModel exportRow, ref int currentRow)
        {
            var row = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow);

            AssignStringData(
                            row,
                             ColumnTime,
                            exportRow.KickOffTime,
                             exportRow.Team,
                             exportRow.EventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}