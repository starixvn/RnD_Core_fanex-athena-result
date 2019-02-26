namespace Fanex.Result.Data
{
    using System;
    using Core.Sportsbook.DTO;
    using NPOI.SS.UserModel;

    public class BetBuilderDetailResultExcelReader : ResultExcelReader<BetBuilderDetail>
    {
        public BetBuilderDetailResultExcelReader(string filePath) : base(filePath)
        {
        }

        public static BetBuilderDetailResultExcelReader CreateInstance(string filePath)
        {
            return new BetBuilderDetailResultExcelReader(filePath);
        }

        protected override BetBuilderDetail MapResultData(IRow row)
        {
            var resultData = new BetBuilderDetail();

            resultData.SelectionType = Convert.ToInt32(row.GetCell(0)?.ToString());
            resultData.Result = row.GetCell(1)?.ToString();

            return resultData;
        }
    }
}