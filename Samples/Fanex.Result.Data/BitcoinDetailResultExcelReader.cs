namespace Fanex.Result.Data
{
    using System;
    using Core.Bitcoin.DTO;
    using NPOI.SS.UserModel;

    public class BitcoinDetailResultExcelReader : ResultExcelReader<GameDetail>
    {
        public BitcoinDetailResultExcelReader(string filePath) : base(filePath)
        {
        }

        public static BitcoinDetailResultExcelReader CreateInstance(string filePath)
        {
            return new BitcoinDetailResultExcelReader(filePath);
        }

        protected override GameDetail MapResultData(IRow row)
        {
            var resultData = new GameDetail
            {
                Match3rdId = Convert.ToInt32(row.GetCell(0)?.ToString()),
                GameResult = row.GetCell(1)?.ToString()
            };

            return resultData;
        }
    }
}