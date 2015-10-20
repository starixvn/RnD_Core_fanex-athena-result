// -----------------------------------------------------------------------
// <copyright file="LiveCasinoExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// ---------------------------------------------------------------------

namespace Fanex.Result.Core.LiveCasino.ExcelModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.ExcelModels;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.LiveCasino.ViewModels;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Live casino excel model class.
    /// </summary>
    internal sealed class LiveCasinoExcel : ExcelBase, IExcelModel
    {
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public LiveCasinoExcel(
            string templatePath,
            ResultSetting setting,
            IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            this.ColumnTemplateCount = 6;
            this.ColumnStatus = 5;
            this.RowContentTemplate = 3;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(LangResource.MatchCode);
            row.Cells[colidx++].SetCellValue(LangResource.KickOffTime);
            row.Cells[colidx++].SetCellValue(LangResource.GameTypeAndTableNumber);
            row.Cells[colidx++].SetCellValue(LangResource.FinalScore);
            row.Cells[colidx++].SetCellValue(LangResource.Results);
            row.Cells[colidx].SetCellValue(LangResource.Status);
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
                var firstItem = (LiveCasinoViewModel)item.FirstOrDefault();
                var rowLeague = sheet.CopyRow(TemplateSheet, RowLeagueTemplate, currentRow, ColumnTemplateIndexList);
                rowLeague.Cells[0].SetCellValue(firstItem.LeagueName);
                sheet.MergeCells(currentRow, 0, currentRow, 1);
                currentRow++;
                if (string.IsNullOrEmpty(firstItem.PlayerAndBanker))
                {
                    rowLeague.Cells[2].SetCellValue(string.Empty);
                }

                foreach (LiveCasinoViewModel exportRow in item)
                {
                    var rowMatch = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow, ColumnTemplateIndexList);

                    currentRow++;

                    AssignStringData(
                        rowMatch,
                        ColumnMatchCode,
                        exportRow.MatchCode,
                        Helper.ToRtfString(exportRow.KickOffTime),
                        exportRow.GameType,
                        exportRow.FinalScore,
                        Helper.ToRtfString(exportRow.Result),
                        exportRow.EventStatus);
                }
            }
        }

        protected override IList<int> GetColumnTemplateIndex()
        {
            return new List<int> { 0, 1, 2, 3, 4, 5 };
        }
    }
}