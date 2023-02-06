namespace Fanex.Result.Core.Sportsbook.ExcelModels
{
    using Base;
    using Base.ExcelModels;
    using Base.Extension;
    using Base.Utilities;
    using Base.ViewModels;
    using NPOI.SS.UserModel;
    using System.Collections.Generic;
    using ViewModels;

    internal sealed class GolfExcel : BilliardExcel
    {
        public GolfExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
        }

        #region Assign Cell Data

        protected override void SetDataForMatch(ISheet sheet, IResultViewModel data, ref int currentRow)
        {
            var exportRow = (GolfViewModel)data;

            var rowHome = sheet.CopyRow(TemplateSheet, GetRowTeamWin(exportRow.HomeWin), currentRow);

            var rowAway = sheet.CopyRow(TemplateSheet, GetRowTeamWin(exportRow.AwayWin), currentRow + 1);

            MergeResultCells(sheet, currentRow, currentRow + 1, ColumnKickOffTime, ColumnStatus);

            AssignStringData(
                rowHome,
                ColumnMatchCode,
                exportRow.MatchCode,
                Helper.ToRtfString(exportRow.KickOffTime),
                Helper.ToRtfString(exportRow.HomeTeam),
                exportRow.FinalHomeScore,
                exportRow.EventStatus);
            currentRow++;

            AssignStringData(
                rowAway,
                ColumnMatchCode,
                exportRow.MatchId,
                Helper.ToRtfString(exportRow.KickOffTime),
                Helper.ToRtfString(exportRow.AwayTeam),
                exportRow.FinalAwayScore,
                exportRow.EventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}