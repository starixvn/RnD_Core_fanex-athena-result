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
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;
    using ViewModels;

    internal sealed class NumberGameExcel : ExcelBase, IExcelModel
    {
        #region Fields

        private const string Match = "{0} {1}";
        private const int MatchColumnIndex = 2;
        private const int StatusColumnIndex = 7;
        private const int FirstBallColumnIndex = 4;
        private const int SecondBallColumnIndex = 5;
        private const int LastBallColumnIndex = 6;
        private const int RowHeight = 700;
        private ICellStyle normalCellStyle;
        private Dictionary<string, ICellStyle> cellStyles = new Dictionary<string, ICellStyle>();
        #endregion Fields

        public NumberGameExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            ColumnTemplateCount = 8;
            ColumnStatus = 7;
            RowContentTemplate = 3;
        }

        protected override void AssignHeader(NPOI.SS.UserModel.ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(LangResource.MatchCode + "/" + LangResource.ID);
            row.Cells[colidx++].SetCellValue(LangResource.KickOffTime);
            row.Cells[colidx++].SetCellValue(LangResource.Match);
            row.Cells[colidx++].SetCellValue(LangResource.LblFTOE);
            row.Cells[colidx++].SetCellValue(LangResource.Lbl1stBall);
            row.Cells[colidx++].SetCellValue(LangResource.Lbl2ndBall);
            row.Cells[colidx++].SetCellValue(LangResource.LblThirdBall);
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
                normalCellStyle = CreateCellStyle("white");
                cellStyles.Add("red", CreateCellStyle("red"));
                cellStyles.Add("blue", CreateCellStyle("blue"));
                cellStyles.Add("disable", CreateCellStyle("disable"));
                foreach (var exportRow in item.ToList().OfType<NumberGameViewModel>())
                {
                    var rowMatch = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow);
                    rowMatch.Height = RowHeight;
                    var firstBallColor = NumberGameViewModel.GetBallColor(exportRow.FirstBall);
                    var seconBallColor = NumberGameViewModel.GetBallColor(exportRow.SecondBall);
                    var lastBallColor = NumberGameViewModel.GetBallColor(exportRow.ThirdBall);

                    SetRowValue(
                        rowMatch,
                        ColumnMatchCode,
                        firstBallColor,
                        seconBallColor,
                        lastBallColor,
                        Helper.ToRtfString(exportRow.MatchCode + "<br/>" + exportRow.MatchId),
                        Helper.ToRtfString(exportRow.KickOffTime),
                        string.Format(Match, LangResource.NumberGameNo, exportRow.GameNo),
                        exportRow.OEFT,
                        exportRow.FirstBall,
                        exportRow.SecondBall,
                        exportRow.ThirdBall,
                        exportRow.EventStatus);
                    currentRow++;
                }
            }
        }

        private ICellStyle CreateCellStyle(string ballColor)
        {
            var cellStyle = Workbook.CreateCellStyle();
            cellStyle.Alignment = HorizontalAlignment.Center;
            cellStyle.VerticalAlignment = VerticalAlignment.Top;
            cellStyle.WrapText = true;
            SetCellStyle(cellStyle, ballColor);

            return cellStyle;
        }

        private void SetRowValue(IRow row, int cellIndex, string firstBallColor, string secondBallColor, string lastBallColor, params string[] values)
        {
            foreach (var t in values)
            {
                var cell = row.CreateCell(cellIndex);
                cell.CellStyle = normalCellStyle;
                if (cellIndex == MatchColumnIndex || cellIndex == StatusColumnIndex)
                {
                    cell.CellStyle.Alignment = HorizontalAlignment.Left;
                }

                if (cellIndex == FirstBallColumnIndex)
                {
                    cell.CellStyle = cellStyles[firstBallColor];
                }

                if (cellIndex == SecondBallColumnIndex)
                {
                    cell.CellStyle = cellStyles[secondBallColor];
                }

                if (cellIndex == LastBallColumnIndex)
                {
                    cell.CellStyle = cellStyles[lastBallColor];
                }

                cell.SetCellValue(t);
                {
                    cellIndex++;
                }
            }
        }

        private static void SetCellStyle(ICellStyle cellStyle, string color)
        {
            cellStyle.FillPattern = FillPattern.SolidForeground;
            if (color == "blue")
            {
                cellStyle.FillBackgroundColor = HSSFColor.LightBlue.Index;
                cellStyle.FillForegroundColor = HSSFColor.LightBlue.Index;
            }
            if (color == "red")
            {
                cellStyle.FillForegroundColor = HSSFColor.Red.Index;
                cellStyle.FillBackgroundColor = HSSFColor.Red.Index;
            }

            if (color == "disable")
            {
                cellStyle.FillForegroundColor = HSSFColor.Grey25Percent.Index;
                cellStyle.FillBackgroundColor = HSSFColor.Grey25Percent.Index;
            }
            if (color == "white")
            {
                cellStyle.FillForegroundColor = HSSFColor.White.Index;
                cellStyle.FillBackgroundColor = HSSFColor.White.Index;
            }
        }
    }
}