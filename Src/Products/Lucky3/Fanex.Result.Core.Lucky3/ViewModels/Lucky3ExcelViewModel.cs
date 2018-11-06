namespace Fanex.Result.Core.Lucky3.ViewModels
{
    using System.Collections.Generic;
    using Base;
    using Base.ExcelModels;
    using Base.Extension;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using NPOI.SS.UserModel;
    using Resources;

    public sealed class Lucky3ExcelViewModel : ExcelBase, IExcelModel
    {
        public Lucky3ExcelViewModel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            ColumnTemplateCount = 8;
            ColumnStatus = 7;
            RowContentTemplate = 4;
            RowLeagueTemplate = 3;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var row = sheet.CopyRow(TemplateSheet, sourceRowIndex: RowHeaderTemplate, destRowIndex: RowHeaderTemplate);
            row.Cells[0].SetCellValue(LangResource.MatchCode + "/" + LangResource.ID);
            row.Cells[1].SetCellValue(LangResource.KickOffTime);
            row.Cells[2].SetCellValue(LangResource.Match);
            row.Cells[3].SetCellValue(Lucky3Resources.DrawNumber);
            row.Cells[4].SetCellValue(Lucky3Resources.NumberOfHits);
            row.Cells[ColumnStatus].SetCellValue(LangResource.Status);

            row = sheet.CopyRow(TemplateSheet, sourceRowIndex: RowHeaderTemplate + 1, destRowIndex: RowHeaderTemplate + 1);
            row.Cells[4].SetCellValue(Lucky3Resources.FirstPrize);
            row.Cells[5].SetCellValue(Lucky3Resources.SecondPrize);
            row.Cells[6].SetCellValue(Lucky3Resources.ThirdPrize);
        }

        protected override void SetDataForMatch(ISheet sheet, IResultViewModel data, ref int currentRow)
        {
#pragma warning disable S3215 // "interface" instances should not be cast to concrete types
            var lucky3Data = data as Lucky3ViewModel;
#pragma warning restore S3215 // "interface" instances should not be cast to concrete types
            var row = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow);

            AssignStringData(
                row,
                ColumnMatchCode,
                Helper.ToRtfString((lucky3Data.MatchCode + "<br/>" + lucky3Data.MatchId)),
                Helper.ToRtfString(lucky3Data.KickOffTime),
                lucky3Data.Match,
                $"{lucky3Data.FirstBall}-{lucky3Data.SecondBall}-{lucky3Data.ThirdBall}",
                lucky3Data.FirstPrize,
                lucky3Data.SecondPrize,
                lucky3Data.ThirdPrize,
                lucky3Data.EventStatus);

            currentRow++;
        }
    }
}