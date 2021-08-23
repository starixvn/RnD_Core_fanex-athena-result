namespace Fanex.Result.Data
{
    using System;
    using Fanex.Result.Core.Bitcoin.DTO;
    using NPOI.SS.UserModel;

    public class BitcoinResultExcelReader : ResultExcelReader<Bitcoin>
    {
        public BitcoinResultExcelReader(string filePath) : base(filePath)
        {
        }

        public static BitcoinResultExcelReader CreateInstance(string filePath)
        {
            return new BitcoinResultExcelReader(filePath);
        }

        protected override Bitcoin MapResultData(IRow row)
        {
            var resultData = new Bitcoin
            {
                Match3rdId = Convert.ToInt32(row.GetCell(0)?.ToString()),
                EventDate = Convert.ToDateTime(row.GetCell(1)?.ToString()),
                EventStatus = row.GetCell(2)?.ToString(),
                League3rdId = Convert.ToInt32(row.GetCell(3)?.ToString()),
                BetStatus = row.GetCell(4)?.ToString()
            };

            return resultData;
        }
    }
}