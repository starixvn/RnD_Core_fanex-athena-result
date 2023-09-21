using System.Runtime.CompilerServices;

namespace Fanex.Result.Core.Bitcoin.ExcelModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.ExcelModels;
    using Base.Extension;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using Fanex.Result.Core.Bitcoin.Resources;
    using NPOI.SS.UserModel;
    using ViewModels;

    internal sealed class BitcoinExcel : ExcelBase, IExcelModel
    {
        public BitcoinExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            ColumnTemplateCount = 4;
            ColumnStatus = 3;
            RowContentTemplate = 3;
        }

        protected override void AssignHeader(NPOI.SS.UserModel.ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(BitcoinResources.Date);
            row.Cells[colidx++].SetCellValue(LangResource.Match);
            row.Cells[colidx++].SetCellValue(BitcoinResources.ResultPrice);
            row.Cells[colidx].SetCellValue(LangResource.Status);
        }

        protected override void AssignSheetData(ISheet sheet)
        {
            var currentRow = RowLeagueTemplate;
            var currentLeagueId = 0;
            var groupData = ViewModelData.GroupBy(i => i.LeagueId).ToList();

            foreach (var item in groupData)
            {
                var firstItem = (BitcoinViewModel)item.FirstOrDefault();
                SetLeague(sheet, firstItem.LeagueId, firstItem.LeagueName, ref currentLeagueId, ref currentRow);
              
                foreach (var exportRow in item.ToList().OfType<BitcoinViewModel>())
                {
                    var columnIndex = 0;
                    var rowMatch = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow++);
                    rowMatch.Height = 600;
                    rowMatch.Cells[columnIndex++].SetCellValue(Helper.ToRtfString(exportRow.KickOffTime));
                    rowMatch.Cells[columnIndex++].SetCellValue(exportRow.MatchId);
                    rowMatch.Cells[columnIndex++].SetCellValue(exportRow.ResultPrice);
                    rowMatch.Cells[columnIndex].SetCellValue(exportRow.EventStatus);
                }
            }
        }
    }
}