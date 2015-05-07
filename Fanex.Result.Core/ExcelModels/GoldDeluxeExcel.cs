// -----------------------------------------------------------------------
// <copyright file="GoldDeluxeoExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// ---------------------------------------------------------------------

namespace Fanex.Result.Core.ExcelModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.Utilities;
    using Fanex.Result.Core.ViewModels;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Live casino excel model class.
    /// </summary>
    internal sealed class GoldDeluxeoExcel : ExcelBase, IExcelModel
    {
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public GoldDeluxeoExcel(
            string templatePath,
            ResultSetting setting,
            IList<IResultViewModel> data,
            LangResources resource)
            : base(templatePath, setting, data, resource)
        {
            this.ColumnTemplateCount = 3;
            this.ColumnStatus = 2;
            this.RowContentTemplate = 3;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(this.Lang.GameID);
            row.Cells[colidx++].SetCellValue(this.Lang.Result);
            row.Cells[colidx++].SetCellValue(this.Lang.Status);
        }

        /// <summary>
        /// Assign value to sheet.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        protected override void AssignSheetData(ISheet sheet)
        {
            var currentRow = RowLeagueTemplate;
            var groupData = this.ViewModelData.GroupBy(i => i.LeagueId);
            foreach (var item in groupData)
            {
                var firstItem = (GoldDeluxeViewModel)item.FirstOrDefault();
                var rowLeague = sheet.CopyRow(TemplateSheet, RowLeagueTemplate, currentRow, ColumnTemplateIndexList);
                rowLeague.Cells[0].SetCellValue(firstItem.SportName + " - " + firstItem.LeagueName);
                sheet.MergeCells(currentRow, 0, currentRow, 1);
                currentRow++;

                foreach (GoldDeluxeViewModel exportRow in item)
                {
                    var rowMatch = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow, ColumnTemplateIndexList);

                    currentRow++;

                    AssignStringData(
                        rowMatch,
                        ColumnMatchCode,
                        exportRow.GameID,
                        exportRow.ResultInExcel,
                        exportRow.EventStatus);
                }
            }
        }

        protected override IList<int> GetColumnTemplateIndex()
        {
            return new List<int> { 0, 1, 2 };
        }
    }
}