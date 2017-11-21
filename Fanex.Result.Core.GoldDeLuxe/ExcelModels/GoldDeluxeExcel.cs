namespace Fanex.Result.Core.GoldDeluxe.ExcelModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.ExcelModels;
    using Base.Extension;
    using Base.Resources;
    using Base.ViewModels;
    using NPOI.SS.UserModel;
    using ViewModels;

    internal sealed class GoldDeluxeExcel : ExcelBase, IExcelModel
    {
        public GoldDeluxeExcel(
            string templatePath,
            ResultSetting setting,
            IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            ColumnTemplateCount = 3;
            ColumnStatus = 2;
            RowContentTemplate = 3;
            base.IsSupportMatchCode = false;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);

            row.Cells[colidx++].SetCellValue(LangResource.GameID);
            row.Cells[colidx++].SetCellValue(LangResource.Result);
            row.Cells[colidx].SetCellValue(LangResource.Status);
        }

        protected override void AssignSheetData(ISheet sheet)
        {
            var currentRow = RowLeagueTemplate;
            var groupData = ViewModelData.GroupBy(i => i.LeagueId).ToList();

            foreach (var item in groupData)
            {
#pragma warning disable S3215 // "interface" instances should not be cast to concrete types
                var firstItem = (GoldDeluxeViewModel)item.FirstOrDefault();
#pragma warning restore S3215 // "interface" instances should not be cast to concrete types
                var rowLeague = sheet.CopyRow(TemplateSheet, RowLeagueTemplate, currentRow, ColumnTemplateIndexList);
                rowLeague.Cells[0].SetCellValue(firstItem.SportName + " - " + firstItem.LeagueName);
                sheet.MergeCells(currentRow, 0, currentRow, 1);
                currentRow++;

                foreach (var exportRow in item.ToList().OfType<GoldDeluxeViewModel>())
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