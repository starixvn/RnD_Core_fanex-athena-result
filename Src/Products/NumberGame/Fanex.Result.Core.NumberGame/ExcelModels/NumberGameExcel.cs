namespace Fanex.Result.Core.NumberGame.ExcelModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.ExcelModels;
    using Base.Extension;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using NPOI.SS.UserModel;
    using ViewModels;

    internal sealed class NumberGameExcel : ExcelBase, IExcelModel
    {
        #region Fields

        private const string Match = "{0} {1}";

        #endregion Fields

        public NumberGameExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            ColumnTemplateCount = 9;
            ColumnStatus = 8;
            RowContentTemplate = 3;
        }

        protected override void AssignHeader(NPOI.SS.UserModel.ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(LangResource.MatchCode + "/" + LangResource.ID);
            row.Cells[colidx++].SetCellValue(LangResource.KickOffTime);
            row.Cells[colidx++].SetCellValue(LangResource.Match);
            row.Cells[colidx++].SetCellValue(LangResource.Results);
            row.Cells[colidx++].SetCellValue(LangResource.Warrior);
            row.Cells[colidx++].SetCellValue(LangResource.LblOEFT);
            row.Cells[colidx++].SetCellValue(LangResource.Lbl1stBall);
            row.Cells[colidx++].SetCellValue(LangResource.LastBall);
            row.Cells[colidx].SetCellValue(LangResource.Status);
        }

        protected override void AssignSheetData(ISheet sheet)
        {
            var currentRow = RowLeagueTemplate;
            var currentLeagueId = 0;
            var groupData = ViewModelData.GroupBy(i => i.LeagueId).ToList();

            foreach (var item in groupData)
            {
#pragma warning disable S3215 // "interface" instances should not be cast to concrete types
                var firstItem = (NumberGameViewModel)item.FirstOrDefault();
#pragma warning restore S3215 // "interface" instances should not be cast to concrete types
                SetLeague(sheet, firstItem.LeagueId, firstItem.LeagueName, ref currentLeagueId, ref currentRow);

                foreach (var exportRow in item.ToList().OfType<NumberGameViewModel>())
                {
                    var rowMatch = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow);

                    var results = new List<string>();

                    foreach (var ball in exportRow.Result)
                    {
                        results.Add(ball);
                    }

                    AssignStringData(
                        rowMatch,
                        ColumnMatchCode,
                        Helper.ToRtfString(exportRow.MatchCode + "<br/>" + exportRow.MatchId),
                        Helper.ToRtfString(exportRow.KickOffTime),
                        string.Format(Match, LangResource.NumberGameNo, exportRow.GameNo),
                        string.Join("-", results),
                        exportRow.Warrior,
                        exportRow.OEFT,
                        exportRow.FirstBall,
                        exportRow.LastBall,
                        exportRow.EventStatus);
                    currentRow++;
                }
            }
        }
    }
}