// -----------------------------------------------------------------------
// <copyright file="NumberGameExcel.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// ---------------------------------------------------------------------

namespace Fanex.Result.Core.NumberGame.ExcelModels
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
    using Fanex.Result.Core.NumberGame.ViewModels;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Number game excel model class.
    /// </summary>
    internal sealed class NumberGameExcel : ExcelBase, IExcelModel
    {
        #region Fields

        private const string Match = "{0} {1}";

        #endregion Fields

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed.")]
        public NumberGameExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            this.ColumnTemplateCount = 9;
            this.ColumnStatus = 8;
            this.RowContentTemplate = 3;
        }

        /// <summary>
        /// Assign value to header section.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
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

        /// <summary>
        /// Assign data to sheet.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        protected override void AssignSheetData(ISheet sheet)
        {
            var currentRow = RowLeagueTemplate;
            var currentLeagueId = 0;
            var groupData = this.ViewModelData.GroupBy(i => i.LeagueId);
            foreach (var item in groupData)
            {
                var firstItem = (NumberGameViewModel)item.FirstOrDefault();
                SetLeague(sheet, firstItem.LeagueId, firstItem.LeagueName, ref currentLeagueId, ref currentRow);

                foreach (NumberGameViewModel exportRow in item)
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