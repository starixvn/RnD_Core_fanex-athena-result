namespace Fanex.Result.Core.VirtualRacing.ExcelModels
{
    using System;
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
    using Fanex.Result.Core.Base.DataDomain;

    internal class VirtualRacingExcel : ExcelBase, IExcelModel
    {
        private const int columnRace = 1;
        private const int ColumnEventTime = 0;
        public VirtualRacingExcel(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
            : base(templatePath, setting, data)
        {
            ColumnTemplateCount = 6;
            ColumnStatus = 5;
            RowLeagueTemplate = 2;
            RowContentTemplate = 3;
            this.IsSupportMatchCode = false;
        }

        protected override void AssignHeader(ISheet sheet)
        {
            var row = sheet.CopyRow(TemplateSheet, RowHeaderTemplate, RowHeaderTemplate);
            var i = ColumnEventTime;

            row.Cells[i++].SetCellValue(LangResource.EventTime);
            row.Cells[i++].SetCellValue(LangResource.Race);
            row.Cells[i++].SetCellValue(LangResource.Place);
            row.Cells[i++].SetCellValue(LangResource.Lane);
            row.Cells[i++].SetCellValue(LangResource.RacerName);
            row.Cells[i++].SetCellValue(LangResource.Status);
        }

        protected override void SetDataForMatch(ISheet sheet, IResultViewModel data, ref int currentRow)
        {
            var exportRow = (VirtualRacingViewModel)data;
            if (exportRow.IsStatusRaceRefund)
            {
                var rowFirst = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow++);
                        AssignStringData(
                            rowFirst,
                            ColumnEventTime,
                            Helper.ToRtfString(exportRow.KickOffTime),
                            exportRow.RaceInformation,
                            exportRow.GetRunnerPrice(),
                            exportRow.GetLane(),
                            exportRow.GetRunnerName(),
                            exportRow.Status);
            }
            else
            {
                var rowspan = exportRow.GetNumberOfDetail <= 0 ? 0 : exportRow.GetNumberOfDetail - 1;
                if (rowspan > 0)
                {
                    sheet.MergeCells(currentRow, ColumnEventTime, currentRow + rowspan, ColumnEventTime);
                    sheet.MergeCells(currentRow, columnRace, currentRow + rowspan, columnRace);
                    sheet.MergeCells(currentRow, ColumnStatus, currentRow + rowspan, ColumnStatus);
                }

                var rowTemplateIndex = 1;
                foreach (var detail in exportRow.Racing.Details)
                {
                    if (rowTemplateIndex == 1)
                    {
                        rowTemplateIndex++;
                        var rowFirst = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow);
                        AssignStringData(
                            rowFirst,
                            ColumnEventTime,
                            Helper.ToRtfString(exportRow.KickOffTime),
                            exportRow.RaceInformation,
                            exportRow.GetRunnerPrice(detail.Placing),
                            exportRow.GetLane(detail.Lane),
                            exportRow.GetRunnerName(detail.RacerId),
                            exportRow.Status);
                    }
                    else if (rowTemplateIndex == 2)
                    {
                        rowTemplateIndex++;
                        var rowSecond = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow + 1);
                        AssignStringData(
                            rowSecond,
                            ColumnEventTime,
                            Helper.ToRtfString(exportRow.KickOffTime),
                            exportRow.RaceInformation,
                            exportRow.GetRunnerPrice(detail.Placing),
                            detail.Lane,
                            exportRow.GetRunnerName(detail.RacerId),
                            exportRow.Status);
                    }
                    else if (rowTemplateIndex == 3)
                    {
                        rowTemplateIndex++;
                        var rowThird = sheet.CopyRow(TemplateSheet, RowContentTemplate, currentRow + 2);
                        AssignStringData(
                            rowThird,
                            ColumnEventTime,
                            Helper.ToRtfString(exportRow.KickOffTime),
                            exportRow.RaceInformation,
                            exportRow.GetRunnerPrice(detail.Placing),
                            detail.Lane,
                            exportRow.GetRunnerName(detail.RacerId),
                            exportRow.Status);
                    }
                }

                currentRow = currentRow + exportRow.Racing.Details.Count;
            }
        }
    }
}