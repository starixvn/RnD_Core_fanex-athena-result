namespace Fanex.Result.Core.Sportsbook.ExcelModels
{
    using System.Collections.Generic;
    using Base;
    using Base.ExcelModels;
    using Base.Extension;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using Fanex.Result.Core.Sportsbook.Resources;
    using NPOI.SS.UserModel;
    using ViewModels;

    internal sealed class VSCockFightingExcel : ExcelBase, IExcelModel
    {
        public VSCockFightingExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            ColumnTemplateCount = 4;
            RowContentTemplate = 3;
            ColumnStatus = 3;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(LangResource.KickOffTime);
            row.Cells[colidx++].SetCellValue(LangResource.Match);
            row.Cells[colidx++].SetCellValue(SportsbookResx.Winner);
            row.Cells[colidx].SetCellValue(LangResource.Status);
        }

        #region Assign Cell Data

        protected override void SetDataForMatch(ISheet sheet, IResultViewModel data, ref int currentRow)
        {
#pragma warning disable S3215 // "interface" instances should not be cast to concrete types
            var exportRow = (VSCockFightingViewModel)data;
#pragma warning restore S3215 // "interface" instances should not be cast to concrete types

            var rowHome = sheet.CopyRow(TemplateSheet, GetRowTeamWin(exportRow.HomeWin), currentRow);
            var rowAway = sheet.CopyRow(TemplateSheet, GetRowTeamWin(exportRow.AwayWin), currentRow + 1);
            const int columnKickOffTime = 0;
            const int columnWinner = 2;
            const int columnTeam = 1;

            sheet.MergeCells(currentRow, columnKickOffTime, currentRow + 1, columnKickOffTime);
            sheet.MergeCells(currentRow, columnWinner, currentRow + 1, columnWinner);
            sheet.MergeCells(currentRow, ColumnStatus, currentRow + 1, ColumnStatus);

            AssignStringData(
                rowHome,
                ColumnMatchCode,
                Helper.ToRtfString(exportRow.KickOffTime),
                Helper.ToRtfString(exportRow.HomeTeam) + exportRow.MatchInfo,
                exportRow.Winner,
                exportRow.EventStatus);
            currentRow++;

            AssignStringData(
                rowAway,
                columnTeam,
                Helper.ToRtfString(exportRow.AwayTeam));
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}