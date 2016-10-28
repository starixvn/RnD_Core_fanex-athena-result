namespace Fanex.Result.Core.Sportsbook.ExcelModels
{
    using System.Collections.Generic;
    using Base;
    using Base.ExcelModels;
    using Base.Extension;
    using Base.Resources;
    using Base.Utilities;
    using Base.ViewModels;
    using NPOI.SS.UserModel;
    using ViewModels;

    internal sealed class OutrightExcel : ExcelBase, IExcelModel
    {
        #region Fields

        private const int ColumnTime = 0;
        private const int ColumnTeam = 1;
        private const int ColumnResults = 2;
        private const int RowNoInfo = 4;

        #endregion Fields

        public OutrightExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            this.ColumnTemplateCount = 3;
            this.ColumnStatus = 4;
            this.RowContentTemplate = 3;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            row.Cells[ColumnTime].SetCellValue(LangResource.Time);
            row.Cells[ColumnTeam].SetCellValue(LangResource.Team);
            row.Cells[ColumnResults].SetCellValue(LangResource.Results);
        }

        protected override void ProcessUI(ISheet sheet)
        {
            /*
            * Void processing UI
            */
        }

        #region Assign Cell Data

        protected override void AssignNoInfoRow(ISheet sheet)
        {
            SetTextRowValue(sheet, RowNoInfo, RowLeagueTemplate, ColumnTemplateCount, LangResource.NoInfo);
        }

        protected override void SetDataForMatch(ISheet sheet, IResultViewModel data, ref int currentRow)
        {
            var exportRow = (OutrightViewModel)data;
            var row = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow);

            AssignStringData(
                row,
                ColumnTime,
                Helper.ToRtfString(exportRow.KickOffTime),
                exportRow.Team,
                exportRow.EventStatus);
            currentRow++;
        }

        #endregion Assign Cell Data
    }
}