// -----------------------------------------------------------------------
// <copyright file="ExcelBase.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// ---------------------------------------------------------------------

namespace Fanex.Result.Core.ExcelModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using Fanex.Result.Core.DataDomain;
    using Fanex.Result.Core.Extension;
    using Fanex.Result.Core.Utilities;
    using Fanex.Result.Core.ViewModels;
    using NPOI.HSSF.UserModel;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Excel base class.
    /// </summary>
    internal abstract class ExcelBase
    {
        #region Fields

        public const int RowTitleTemplate = 0;

        public const int RowHeaderTemplate = 1;

        protected const string FormatDate = "MM/dd/yyyy";

        protected const int RowLeagueTemplate = 2;

        protected const int RowNoInfoTemplate = 4;
        protected const string Na = "N/A";
        protected const int ColumnMatchCode = 0;
        protected const int ColumnRace = 0;
        protected const int ColumnKickOffTime = 1;

        private int _rowContentTemplate = 3;

        private int _columnStatus = 4;

        #endregion Fields

        protected ExcelBase(string _templatePath, ResultSetting _setting, IList<IResultViewModel> _data, LangResources _resource)
        {
            this.TemplatePath = _templatePath;
            this.Lang = _resource;
            this.ReportTitle = _setting.ExcelTitle;
            this.Setting = _setting;
            this.ViewModelData = _data;
            this.NumofRow = _data.Count;
        }

        protected int NumofRow { get; set; }

        protected LangResources Lang { get; set; }

        protected IList<IResultViewModel> ViewModelData { get; set; }

        protected ResultSetting Setting { get; set; }

        protected IWorkbook Workbook { get; set; }

        protected ISheet TemplateSheet { get; set; }

        protected ICellStyle DefaultStyle { get; set; }

        protected IList<int> ColumnTemplateIndexList { get; set; }

        protected string TemplatePath { get; set; }

        protected string ReportTitle { get; set; }

        protected int ColumnStatus
        {
            get { return this._columnStatus; }
            set { this._columnStatus = value; }
        }

        protected int RowContentTemplate
        {
            get { return this._rowContentTemplate; }
            set { this._rowContentTemplate = value; }
        }

        protected int ColumnTemplateCount { get; set; }

        #region Export

        public MemoryStream Export()
        {
            this.LoadTemplate();
            this.AssignData();
            this.RemoveTemplate();
            var excelStream = new MemoryStream();
            this.Workbook.Write(excelStream);
            return excelStream;
        }

        #endregion Export

        #region Export Excel Methods

        /// <summary>
        ///     Clear the template from header row.
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

        /// <summary>
        /// Render excel file.
        /// 1.Render header.
        /// 2.Render title.
        /// 3.Render content.
        /// 4.Process UI.
        /// </summary>
        protected virtual void AssignData()
        {
            var sheet = this.Workbook.CloneSheet("Template", "Data");
            this.ColumnTemplateIndexList = this.GetColumnTemplateIndex();
            this.AssignHeader(sheet);
            this.AssignTitle(sheet);

            if (NumofRow == 0)
            {
                this.AssignNoInfoRow(sheet);
            }
            else
            {
                this.AssignSheetData(sheet);
            }

            // Hide unnecessary column
            this.ProcessUI(sheet);
        }

        /// <summary>
        /// Redder header column.
        /// </summary>
        /// <param name="sheet">Current Sheet.</param>
        protected abstract void AssignHeader(ISheet sheet);

        protected virtual void AssignNoInfoRow(ISheet sheet)
        {
            SetTextRowValue(sheet, RowNoInfoTemplate, RowLeagueTemplate, ColumnTemplateCount, this.Lang.NoInfo);
        }

        /// <summary>
        /// Assign data to sheet.
        /// </summary>
        /// <param name="sheet">The current sheet.</param>
        protected virtual void AssignSheetData(ISheet sheet)
        {
            var currentRow = RowLeagueTemplate;
            var currentLeagueId = 0;
            var groupData = this.ViewModelData.GroupBy(i => i.LeagueId);
            foreach (var item in groupData)
            {
                var firstItem = item.FirstOrDefault();

                SetLeague(sheet, firstItem.LeagueId, firstItem.LeagueName, ref currentLeagueId, ref currentRow);

                foreach (var exportRow in item)
                {
                    SetDataForMatch(sheet, exportRow, ref currentRow);
                }
            }
        }

        protected virtual void SetDataForMatch(ISheet sheet, IResultViewModel data, ref int currentRow)
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

        /// <summary>
        /// Load template.
        /// </summary>
        protected virtual void LoadTemplate()
        {
            FileStream stream = null;
            try
            {
                stream = new FileStream(this.TemplatePath, FileMode.Open, FileAccess.Read);
                this.Workbook = new HSSFWorkbook(stream);
                this.TemplateSheet = this.Workbook.GetSheet("Template");
                this.DefaultStyle = this.Workbook.CreateCellStyle();
                this.DefaultStyle.ShrinkToFit = true;
                this.DefaultStyle.WrapText = true;
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
            this.Workbook.RemoveSheet("Template");
        }

        #endregion Template

        protected virtual void MergeResultCells(ISheet sheet, int currentRow, int rowSpan, int colSpanKichOffTime, int colSpanStatus)
        {
            sheet.MergeCells(currentRow, colSpanKichOffTime, rowSpan, colSpanKichOffTime);
            sheet.MergeCells(currentRow, colSpanStatus, rowSpan, colSpanStatus);
        }

        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", Justification = "Reviewed.")]
        protected virtual void SetLeague(ISheet sheet, int leagueId, string leagueName, ref int currentLeagueId, ref int currentRow)
        {
            if (currentLeagueId == 0 || currentLeagueId != leagueId)
            {
                currentLeagueId = leagueId;
                SetTextRowValue(sheet, 2, currentRow, ColumnTemplateCount, leagueName.ToUpper());
                currentRow++;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Reviews.")]
        protected virtual IList<int> GetColumnTemplateIndex()
        {
            return new List<int>();
        }

        /// <summary>
        /// Get row team win.
        /// </summary>
        /// <param name="finalHomeScore">The final home score.</param>
        /// <param name="finalAwayScore">The final away score.</param>
        /// <returns>The row list.</returns>
        protected virtual int[] GetRowTeamWin(string finalHomeScore, string finalAwayScore)
        {
            var arrRowTeam = new int[2];

            if (string.IsNullOrEmpty(finalAwayScore) || string.IsNullOrEmpty(finalAwayScore))
            {
                arrRowTeam[0] = 4;
                arrRowTeam[1] = 4;
            }
            else
            {
                int homeScore = Helper.IsIntegerNumber(finalHomeScore) ? int.Parse(finalHomeScore) : 0;
                int awayScore = Helper.IsIntegerNumber(finalAwayScore) ? int.Parse(finalHomeScore) : 0;

                /*-----------------------------------------------------
                 * Row 3 is row in excel template formated bold
                 * Row 4 is row in excel template formated no bold
                 -----------------------------------------------------*/
                if (homeScore > awayScore)
                {
                    arrRowTeam[0] = 3;
                    arrRowTeam[1] = 4;
                }
                else if (homeScore < awayScore)
                {
                    arrRowTeam[0] = 4;
                    arrRowTeam[1] = 3;
                }
                else if (homeScore == awayScore)
                {
                    arrRowTeam[0] = 4;
                    arrRowTeam[1] = 4;
                }
            }

            return arrRowTeam;
        }

        #endregion Export Excel Methods

        #region Set Content Excel

        protected void SetTextRowValue(ISheet sheet, int sourceRowIndex, int destRowIndex, int columnCount, string value)
        {
            checked
            {
                var destColumn = columnCount - 1;

                var row = sheet.CopyRow(this.TemplateSheet, sourceRowIndex, destRowIndex);
                sheet.MergeCells(destRowIndex, 0, destRowIndex, destColumn);
                row.Cells[0].SetCellValue(value);
            }
        }

        /// <summary>
        ///     Assign array decimal to row.
        /// </summary>
        /// <param name="row">Row in sheet export excel.</param>
        /// <param name="cellIndex">Position cell will insert.</param>
        /// <param name="decimalValues">Array decimal values.</param>
        /// <returns>Cell position after insert.</returns>
        protected virtual int AssignDecimalData(IRow row, int cellIndex, params decimal[] decimalValues)
        {
            for (int i = 0, j = decimalValues.Length; i < j && cellIndex < row.Cells.Count; i++)
            {
                row.Cells[cellIndex].SetCellValue(decimalValues[i]);
                cellIndex++;
            }

            return cellIndex;
        }

        /// <summary>
        ///     Assign array string to row.
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
                checked
                {
                    cellIndex++;
                }
            }

            return cellIndex;
        }

        #endregion Set Content Excel
    }
}