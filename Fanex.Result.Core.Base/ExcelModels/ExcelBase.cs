namespace Fanex.Result.Core.Base.ExcelModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using Extension;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;
    using Resources;
    using ViewModels;

    public abstract class ExcelBase
    {
        #region Fields

        public const int RowTitleTemplate = 0;
        public const int RowHeaderTemplate = 1;

        protected const string FormatDate = "MM/dd/yyyy";
        protected const int BoldTemplateRow = 3;
        protected const int NoneBoldTemplateRow = 4;
        protected const int RowNoInfoTemplate = 5;
        protected const string Na = "N/A";
        protected const int ColumnMatchCode = 0;
        protected const int ColumnRace = 0;
        protected const int ColumnKickOffTime = 1;

        #endregion Fields

        protected ExcelBase(string templatePath, ResultSetting setting, IList<IResultViewModel> data)
        {
            TemplatePath = templatePath;

            ReportTitle = setting.ExcelTitle;
            Setting = setting;
            ViewModelData = data;
            NumofRow = data.Count;
        }

        protected internal int NumofRow { get; set; }

        protected internal IList<IResultViewModel> ViewModelData { get; set; }

        protected internal ResultSetting Setting { get; set; }

        protected internal IWorkbook Workbook { get; set; }

        protected internal ISheet TemplateSheet { get; set; }

        protected internal ICellStyle DefaultStyle { get; set; }

        protected internal IList<int> ColumnTemplateIndexList { get; set; }

        protected internal string TemplatePath { get; set; }

        protected internal string ReportTitle { get; set; }

        protected internal int ColumnStatus { get; set; } = 4;

        protected internal int RowContentTemplate { get; set; } = 3;

        protected internal int ColumnTemplateCount { get; set; }

        protected internal int RowLeagueTemplate { get; set; } = 2;

        #region Export

        public MemoryStream Export()
        {
            LoadTemplate();
            AssignData();
            RemoveTemplate();
            var excelStream = new MemoryStream();
            Workbook.Write(excelStream);

            return excelStream;
        }

        #endregion Export

        #region Export Excel Methods

        /// <summary>
        /// Clear the template from header row.
        /// </summary>
        /// <param name="sheet">Current sheet.</param>
        /// <param name="startRowIndex">Index of row.</param>
        protected static void ClearTemplate(ISheet sheet, int startRowIndex)
        {
            var lastRowNum = sheet.LastRowNum;

            for (var i = startRowIndex; i <= lastRowNum; i++)
            {
                sheet.DeleteRow(i);
            }
        }

        protected virtual void AssignTitle(ISheet sheet)
        {
            sheet.GetCell(0, 0).SetCellValue(ReportTitle);

            ClearTemplate(sheet, RowLeagueTemplate);

            sheet.CreateFreezePane(0, RowLeagueTemplate);
        }

        protected virtual void AssignData()
        {
            var sheet = Workbook.CloneSheet("Template", "Data");
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

        protected abstract void AssignHeader(ISheet sheet);

        protected virtual void AssignNoInfoRow(ISheet sheet)
        {
            SetTextRowValue(sheet, RowNoInfoTemplate, RowLeagueTemplate, ColumnTemplateCount, LangResource.NoInfo);
        }

        protected virtual void AssignSheetData(ISheet sheet)
        {
            var currentRow = RowLeagueTemplate;
            var currentLeagueId = 0;
            var groupData = ViewModelData.GroupBy(i => i.LeagueId).ToList();

            foreach (var item in groupData)
            {
                var firstItem = item.FirstOrDefault();

                SetLeague(sheet, firstItem.LeagueId, firstItem.LeagueName, ref currentLeagueId, ref currentRow);

                foreach (var exportRow in item.ToList())
                {
                    SetDataForMatch(sheet, exportRow, ref currentRow);
                }
            }
        }

#pragma warning disable S3874 // "out" and "ref" parameters should not be used

        protected virtual void SetDataForMatch(ISheet sheet, IResultViewModel data, ref int currentRow)
#pragma warning restore S3874 // "out" and "ref" parameters should not be used
        {
        }

        protected virtual void ProcessUI(ISheet sheet)
        {
            if (!Setting.VisibleMatchCodeIDColumn)
            {
                sheet.SetColumnHidden(ColumnMatchCode, true);
            }
        }

        #region Template

        protected virtual void LoadTemplate()
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(TemplatePath, FileMode.Open, FileAccess.Read);
                Workbook = new HSSFWorkbook(stream);
                TemplateSheet = Workbook.GetSheet("Template");
                DefaultStyle = Workbook.CreateCellStyle();
                DefaultStyle.ShrinkToFit = true;
                DefaultStyle.WrapText = true;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }

        protected virtual void RemoveTemplate()
        {
            Workbook.RemoveSheet("Template");
        }

        #endregion Template

        protected virtual void MergeResultCells(ISheet sheet, int currentRow, int rowSpan, int colSpanKichOffTime, int colSpanStatus)
        {
            sheet.MergeCells(currentRow, colSpanKichOffTime, rowSpan, colSpanKichOffTime);
            sheet.MergeCells(currentRow, colSpanStatus, rowSpan, colSpanStatus);
        }

#pragma warning disable S3874 // "out" and "ref" parameters should not be used

        protected virtual void SetLeague(ISheet sheet, int leagueId, string leagueName, ref int currentLeagueId, ref int currentRow)
#pragma warning restore S3874 // "out" and "ref" parameters should not be used
        {
            if (currentLeagueId == 0 || currentLeagueId != leagueId)
            {
                currentLeagueId = leagueId;
                SetTextRowValue(sheet, RowLeagueTemplate, currentRow, ColumnTemplateCount, leagueName.ToUpperInvariant());
                currentRow++;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Reviews.")]
        protected virtual IList<int> GetColumnTemplateIndex()
        {
            return new List<int>();
        }

        protected virtual int GetRowTeamWin(string winFormatted)
        {
            return this.Setting.HighlightWinnerTeam && !string.IsNullOrWhiteSpace(winFormatted) ? BoldTemplateRow : NoneBoldTemplateRow;
        }

        #endregion Export Excel Methods

        #region Set Content Excel

        protected void SetTextRowValue(ISheet sheet, int sourceRowIndex, int destRowIndex, int columnCount, string value)
        {
            checked
            {
                var destColumn = columnCount - 1;
                var row = sheet.CopyRow(TemplateSheet, sourceRowIndex, destRowIndex);
                sheet.MergeCells(destRowIndex, 0, destRowIndex, destColumn);
                row.Cells[0].SetCellValue(value);
            }
        }

        /// <summary>
        /// Assign array decimal to row.
        /// </summary>
        /// <param name="row">Row in sheet export excel.</param>
        /// <param name="cellIndex">Position cell will insert.</param>
        /// <param name="decimalValues">Array decimal values.</param>
        /// <returns>Cell position after insert.</returns>
        protected virtual int AssignDecimalData(IRow row, int cellIndex, params decimal[] decimalValues)
        {
            for (int i = 0; i < decimalValues.Length && cellIndex < row.Cells.Count; i++)
            {
                row.Cells[cellIndex].SetCellValue(decimalValues[i]);
                cellIndex++;
            }

            return cellIndex;
        }

        /// <summary>
        /// Assign array string to row.
        /// </summary>
        /// <param name="row">Row in sheet export excel.</param>
        /// <param name="cellIndex">Position cell will insert.</param>
        /// <param name="values">Array string values.</param>
        /// <returns>Cell position after insert.</returns>
        protected virtual int AssignStringData(IRow row, int cellIndex, params string[] values)
        {
            foreach (var t in values)
            {
                row.Cells[cellIndex].SetCellValue(t);
                {
                    cellIndex++;
                }
            }

            return cellIndex;
        }

        #endregion Set Content Excel
    }
}