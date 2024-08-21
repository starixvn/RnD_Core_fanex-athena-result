namespace Fanex.Result.Core.Sportsbook.ExcelModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Base.ExcelModels;
    using Base.Extension;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using Fanex.Result.Core.Sportsbook.Resources;
    using NPOI.SS.UserModel;
    using ViewModels;

    internal sealed class BaseBallExcel : ExcelBase, IExcelModel
    {
        private const int ColumnStatusForSeriesMatch = 11;
        private const int ColumnHTScore = 13;
        private BaseBallViewModel FirstMatch;
        public BaseBallExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            ColumnTemplateCount = 16;
            ColumnStatus = 15;
            RowContentTemplate = 3;
            FirstMatch = (BaseBallViewModel)data.GroupBy(i => i.LeagueId).ToList()[0].FirstOrDefault();
        }

        protected override void AssignData()
        {
            TemplateSheet = FirstMatch.IsSeriesMatch ? Workbook.GetSheet("Series") : Workbook.GetSheet("Regular");
            var sheet = FirstMatch.IsSeriesMatch ? Workbook.CloneSheet("Series", "Data") : Workbook.CloneSheet("Regular", "Data");

            ColumnTemplateIndexList = GetColumnTemplateIndex();
            AssignHeader(sheet);
            AssignTitle(sheet);

            if (NumofRow == 0)
            {
                AssignNoInfoRow(sheet);
            }
            else
            {
                AssignSheetData(sheet);
            }

            // Hide unnecessary column
            ProcessUI(sheet);
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(LangResource.MatchCode + "/" + LangResource.ID);
            row.Cells[colidx++].SetCellValue(LangResource.KickOffTime);

            if (FirstMatch.IsSeriesMatch)
            {
                row.Cells[colidx++].SetCellValue(SportsbookResx.SeriesMatch);
                row.Cells[colidx++].SetCellValue(LangResource.BaseballGame01);
                row.Cells[colidx++].SetCellValue(LangResource.BaseballGame02);
                row.Cells[colidx++].SetCellValue(LangResource.BaseballGame03);
                row.Cells[colidx++].SetCellValue(LangResource.BaseballGame04);
                row.Cells[colidx++].SetCellValue(LangResource.BaseballGame05);
                row.Cells[colidx++].SetCellValue(LangResource.BaseballGame06);
                row.Cells[colidx++].SetCellValue(LangResource.BaseballGame07);
                row.Cells[colidx++].SetCellValue(LangResource.BaseballTotalGames);
                row.Cells[colidx].SetCellValue(LangResource.Status);
            }
            else
            {
                row.Cells[colidx].SetCellValue(LangResource.Match);
                colidx = ColumnHTScore;
                row.Cells[colidx - 1].SetCellValue(LangResource.EI);
                row.Cells[colidx++].SetCellValue(SportsbookResx.Runs);
                row.Cells[colidx++].SetCellValue(SportsbookResx.Hits);
                row.Cells[colidx].SetCellValue(LangResource.Status);
            }
        }

        protected override void AssignNoInfoRow(ISheet sheet)
        {
            var numberCell = FirstMatch.IsSeriesMatch ? ColumnStatusForSeriesMatch + 1 : ColumnTemplateCount;
            SetTextRowValue(sheet, RowNoInfoTemplate, RowLeagueTemplate, numberCell, LangResource.NoInfo);
        }

        #region Assign Cell Data

        protected override void SetLeague(ISheet sheet, int leagueId, string leagueName, ref int currentLeagueId, ref int currentRow)
        {
            if (currentLeagueId == 0 || currentLeagueId != leagueId)
            {
                currentLeagueId = leagueId;
                var numberCell = FirstMatch.IsSeriesMatch ? ColumnStatusForSeriesMatch + 1 : ColumnTemplateCount;
                SetTextRowValue(sheet, RowLeagueTemplate, currentRow, numberCell, leagueName.ToUpperInvariant());
                currentRow++;
            }
        }

        protected override void SetDataForMatch(ISheet sheet, IResultViewModel data, ref int currentRow)
        {
            var exportRow = (BaseBallViewModel)data;
            var rowHome = sheet.CopyRow(TemplateSheet, GetRowTeamWin(exportRow.HomeWin), currentRow);
            var rowAway = sheet.CopyRow(TemplateSheet, GetRowTeamWin(exportRow.AwayWin), currentRow + 1);

            if (FirstMatch.IsSeriesMatch)
            {
                MergeResultCells(sheet, currentRow, currentRow + 1, ColumnKickOffTime, ColumnStatusForSeriesMatch);
                AssignStringData(
                    rowHome,
                    ColumnMatchCode,
                    exportRow.MatchCode,
                    Helper.ToRtfString(exportRow.KickOffTime),
                    Helper.ToRtfString(exportRow.HomeTeam),
                    exportRow.GetHomeAwaySet(1, true),
                    exportRow.GetHomeAwaySet(2, true),
                    exportRow.GetHomeAwaySet(3, true),
                    exportRow.GetHomeAwaySet(4, true),
                    exportRow.GetHomeAwaySet(5, true),
                    exportRow.GetHomeAwaySet(6, true),
                    exportRow.GetHomeAwaySet(7, true),
                    exportRow.FinalHomeScore,
                    exportRow.EventStatus);
                currentRow++;

                AssignStringData(
                    rowAway,
                    ColumnMatchCode,
                    exportRow.MatchId,
                    Helper.ToRtfString(exportRow.KickOffTime),
                    Helper.ToRtfString(exportRow.AwayTeam),
                    exportRow.GetHomeAwaySet(1, false),
                    exportRow.GetHomeAwaySet(2, false),
                    exportRow.GetHomeAwaySet(3, false),
                    exportRow.GetHomeAwaySet(4, false),
                    exportRow.GetHomeAwaySet(5, false),
                    exportRow.GetHomeAwaySet(6, false),
                    exportRow.GetHomeAwaySet(7, false),
                    exportRow.FinalAwayScore,
                    exportRow.EventStatus);
                currentRow++;
            }
            else
            {
                MergeResultCells(sheet, currentRow, currentRow + 1, ColumnKickOffTime, ColumnStatus);
                AssignStringData(
                    rowHome,
                    ColumnMatchCode,
                    exportRow.MatchCode,
                    Helper.ToRtfString(exportRow.KickOffTime),
                    Helper.ToRtfString(exportRow.HomeTeam),
                    exportRow.GetHomeAwaySet(1, true),
                    exportRow.GetHomeAwaySet(2, true),
                    exportRow.GetHomeAwaySet(3, true),
                    exportRow.GetHomeAwaySet(4, true),
                    exportRow.GetHomeAwaySet(5, true),
                    exportRow.GetHomeAwaySet(6, true),
                    exportRow.GetHomeAwaySet(7, true),
                    exportRow.GetHomeAwaySet(8, true),
                    exportRow.GetHomeAwaySet(9, true),
                    exportRow.HomeExtraTime,
                    exportRow.FinalHomeScore,
                    exportRow.HomeHits,
                    exportRow.EventStatus);
                currentRow++;

                AssignStringData(
                    rowAway,
                    ColumnMatchCode,
                    exportRow.MatchId,
                    Helper.ToRtfString(exportRow.KickOffTime),
                    Helper.ToRtfString(exportRow.AwayTeam),
                    exportRow.GetHomeAwaySet(1, false),
                    exportRow.GetHomeAwaySet(2, false),
                    exportRow.GetHomeAwaySet(3, false),
                    exportRow.GetHomeAwaySet(4, false),
                    exportRow.GetHomeAwaySet(5, false),
                    exportRow.GetHomeAwaySet(6, false),
                    exportRow.GetHomeAwaySet(7, false),
                    exportRow.GetHomeAwaySet(8, false),
                    exportRow.GetHomeAwaySet(9, false),
                    exportRow.AwayExtraTime,
                    exportRow.FinalAwayScore,
                    exportRow.AwayHits,
                    exportRow.EventStatus);
                currentRow++;
            }

        }

        protected override void RemoveTemplate()
        {
            Workbook.RemoveSheet("Regular");
            Workbook.RemoveSheet("Series");
        }
        #endregion Assign Cell Data
    }
}