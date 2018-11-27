namespace Fanex.Result.Data
{
    using System;
    using Fanex.Result.Core.GoldDeluxe.DTO;
    using NPOI.SS.UserModel;

    public class GoldDeluxeResultExcelReader : ResultExcelReader<GoldDeluxe>
    {
        public GoldDeluxeResultExcelReader(string filePath) : base(filePath)
        {
        }

        public static GoldDeluxeResultExcelReader CreateInstance(string filePath)
        {
            return new GoldDeluxeResultExcelReader(filePath);
        }

        protected override GoldDeluxe MapResultData(IRow row)
        {
            var resultData = new GoldDeluxe();

            resultData.Bettype = Convert.ToInt32(row.GetCell(0)?.ToString());
            resultData.Result = row.GetCell(1)?.ToString();

            return resultData;
        }
    }
}