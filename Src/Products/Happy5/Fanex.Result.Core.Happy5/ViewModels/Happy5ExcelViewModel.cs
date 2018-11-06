namespace Fanex.Result.Core.Happy5.ViewModels
{
    using System.Collections.Generic;
    using Base;
    using Base.ExcelModels;
    using Base.Extension;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using Fanex.Result.Core.Happy5.DataDomain;
    using Fanex.Result.Core.Happy5.Resources;
    using NPOI.SS.UserModel;

    public sealed class Happy5ExcelViewModel : ExcelBase, IExcelModel
    {
        public Happy5ExcelViewModel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            ColumnTemplateCount = 11;
            ColumnStatus = 10;
            RowContentTemplate = 3;
            RowLeagueTemplate = 2;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var row = sheet.CopyRow(TemplateSheet, sourceRowIndex: RowHeaderTemplate, destRowIndex: RowHeaderTemplate);
            row.Cells[0].SetCellValue(LangResource.MatchCode + "/" + LangResource.ID);
            row.Cells[1].SetCellValue(LangResource.KickOffTime);
            row.Cells[2].SetCellValue(LangResource.Match);
            row.Cells[3].SetCellValue(LangResource.Result);
            row.Cells[4].SetCellValue(Happy5Resources.Total);
            row.Cells[5].SetCellValue(Constant.BigSmall);
            row.Cells[6].SetCellValue(Constant.OddEven);
            row.Cells[7].SetCellValue(Constant.FourSeasons);
            row.Cells[8].SetCellValue(Constant.MoreOddMoreEven);
            row.Cells[9].SetCellValue(Constant.Combo);
            row.Cells[ColumnStatus].SetCellValue(LangResource.Status);
        }

#pragma warning disable S3215 // "interface" instances should not be cast to concrete types

        protected override void SetDataForMatch(ISheet sheet, IResultViewModel data, ref int currentRow)
        {
            var happy5Data = data as Happy5ViewModel;
            var row = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow);

            AssignStringData(
                row,
                ColumnMatchCode,
                Helper.ToRtfString((happy5Data.MatchCode + "<br/>" + happy5Data.MatchId)),
                Helper.ToRtfString(happy5Data.KickOffTime),
                happy5Data.Match,
                GetResult(happy5Data),
                happy5Data.TotalBall,
                happy5Data.BigSmall,
                happy5Data.OddEven,
                happy5Data.FourSeason,
                happy5Data.MoreOddMoreEven,
                happy5Data.Combo,
                happy5Data.EventStatus);

            currentRow++;
        }

#pragma warning restore S3215 // "interface" instances should not be cast to concrete types

        private static string GetResult(Happy5ViewModel happy5Data)
            => happy5Data.EventStatusIsRefundOrPending ?
                Constant.NA :
                $"{happy5Data.FirstBall}-{happy5Data.SecondBall}-{happy5Data.ThirdBall}-{happy5Data.FourthBall}-{happy5Data.FifthBall}";
    }
}