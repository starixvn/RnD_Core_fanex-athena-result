namespace Fanex.Result.Core.Sportsbook.ExcelModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.ExcelModels;
    using Fanex.Result.Core.Base.Extension;
    using Fanex.Result.Core.Base.Resources;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Base.ViewModels;
    using Fanex.Result.Core.Sportsbook.ViewModels;
    using NPOI.SS.UserModel;

    internal sealed class MuayThaiExcel : ExcelBase, IExcelModel
    {
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public MuayThaiExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            this.ColumnTemplateCount = 5;
            this.ColumnStatus = 4;
            this.RowContentTemplate = 3;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var colidx = 0;
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[colidx++].SetCellValue(LangResource.MatchCode + "/" + LangResource.ID);
            row.Cells[colidx++].SetCellValue(LangResource.KickOffTime);
            row.Cells[colidx++].SetCellValue(LangResource.Match);
            row.Cells[colidx++].SetCellValue(LangResource.FinalScore);
            row.Cells[colidx].SetCellValue(LangResource.Status);
        }

        #region Assign Cell Data

        /// <summary>
        /// Set data for match.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        /// <param name="data">The view model.</param>
        /// <param name="currentRow">The current row.</param>
        protected override void SetDataForMatch(ISheet sheet, IResultViewModel data, ref int currentRow)
        {
            var exportRow = (MuayThaiViewModel)data;
            var rowHome = sheet.CopyRow(TemplateSheet, this.GetRowTeamWin(exportRow.HomeWin), currentRow);

            var rowAway = sheet.CopyRow(TemplateSheet, this.GetRowTeamWin(exportRow.AwayWin), currentRow + 1);

            MergeResultCells(sheet, currentRow, currentRow + 1, ColumnKickOffTime, this.ColumnStatus);

            var eventStatus = exportRow.EventStatus;
            if (!string.IsNullOrEmpty(exportRow.KOInRound))
            {
                eventStatus += "\n " + exportRow.KOInRound;
            }

            AssignStringData(
                rowHome,
                             ColumnMatchCode,
                             exportRow.MatchCode,
                            Helper.ToRtfString(exportRow.KickOffTime),
                              Helper.ToRtfString(exportRow.HomeTeam),
                       exportRow.FinalHomeScore,
                             eventStatus);
            currentRow++;

            AssignStringData(
                rowAway,
                             ColumnMatchCode,
                             exportRow.MatchId,
                             Helper.ToRtfString(exportRow.KickOffTime),
                             Helper.ToRtfString(exportRow.AwayTeam),
                       exportRow.FinalAwayScore,
                             eventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}