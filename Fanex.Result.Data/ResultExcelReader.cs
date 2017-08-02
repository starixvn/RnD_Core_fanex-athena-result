namespace Fanex.Result.Data
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using NPOI.HSSF.UserModel;
    using NPOI.POIFS.FileSystem;
    using NPOI.SS.UserModel;

    public abstract class ResultExcelReader<T> where T : new()
    {
        private readonly string excelFilePath;
        private IWorkbook workbook;
        private IFormulaEvaluator formulaEvaluator;
        private DataFormatter dataFormatter;

        protected ResultExcelReader(string filePath)
        {
            excelFilePath = filePath;
        }

        public IEnumerable<T> GetResultData()
        {
            LoadWorkbook();
            var sheet = workbook.GetSheetAt(0);
            var resultData = new List<T>();

            if (sheet != null)
            {
                sheet.RemoveRow(sheet.GetRow(0));

                resultData = GetResultData(sheet).ToList();
            }

            return resultData;
        }

        protected virtual IEnumerable<T> GetResultData(ISheet sheet)
        {
            var resultData = new List<T>();

            foreach (IRow row in sheet)
            {
                var data = MapResultData(row);

                if (!Equals(data, default(T)))
                {
                    resultData.Add(data);
                }
            }

            return resultData;
        }

        protected abstract T MapResultData(IRow row);

        protected string GetCellValue(ICell cell)
        {
            string ret = string.Empty;

            if (cell == null)
            {
                return ret;
            }

            ret = dataFormatter.FormatCellValue(cell, formulaEvaluator);

            // remove line break
            return ret.Replace("\n", " ");
        }

        protected static bool ConvertStringToBoolean(string value)
           => !string.IsNullOrEmpty(value) && value != "0";

        private void LoadWorkbook()
        {
            using (StreamReader input = new StreamReader(excelFilePath))
            {
                workbook = new HSSFWorkbook(new POIFSFileSystem(input.BaseStream));
                formulaEvaluator = new HSSFFormulaEvaluator(workbook);
                dataFormatter = new HSSFDataFormatter(new CultureInfo("en-US"));
            }
        }
    }
}