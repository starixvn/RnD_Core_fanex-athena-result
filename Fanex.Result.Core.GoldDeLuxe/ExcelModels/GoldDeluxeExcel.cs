// -----------------------------------------------------------------------
// <copyright file="GoldDeluxeExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// ---------------------------------------------------------------------

namespace Fanex.Result.Core.GoldDeluxe.ExcelModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.ExcelModels;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.GoldDeluxe.ViewModels;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Live casino excel model class.
    /// </summary>
    internal sealed class GoldDeluxeExcel : ExcelBase, IExcelModel
    {
        public GoldDeluxeExcel(
            string templatePath,
            ResultSetting setting,
            IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            this.ColumnTemplateCount = 3;
            this.ColumnStatus = 2;
            this.RowContentTemplate = 3;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(LangResource.GameID);
            row.Cells[colidx++].SetCellValue(LangResource.Result);
            row.Cells[colidx++].SetCellValue(LangResource.Status);
        }

        /// <summary>
        /// Assign value to sheet.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        protected override void AssignSheetData(ISheet sheet)
        {
            var currentRow = RowLeagueTemplate;
            var groupData = this.ViewModelData.GroupBy(i => i.LeagueId).ToList();
            foreach (var item in groupData)
            {
                var firstItem = (GoldDeluxeViewModel)item.FirstOrDefault();
                var rowLeague = sheet.CopyRow(TemplateSheet, RowLeagueTemplate, currentRow, ColumnTemplateIndexList);
                rowLeague.Cells[0].SetCellValue(firstItem.SportName + " - " + firstItem.LeagueName);
                sheet.MergeCells(currentRow, 0, currentRow, 1);
                currentRow++;

                foreach (GoldDeluxeViewModel exportRow in item.ToList())
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