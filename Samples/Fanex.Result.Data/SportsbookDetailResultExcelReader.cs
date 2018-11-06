namespace Fanex.Result.Data
{
    using System;
    using Core.Sportsbook.DTO;
    using NPOI.SS.UserModel;

    public class SportsbookDetailResultExcelReader : ResultExcelReader<GameDetail>
    {
        public SportsbookDetailResultExcelReader(string filePath) : base(filePath)
        {
        }

        public static SportsbookDetailResultExcelReader CreateInstance(string filePath)
        {
            return new SportsbookDetailResultExcelReader(filePath);
        }

        protected override GameDetail MapResultData(IRow row)
        {
            var resultData = new GameDetail();

            resultData.MatchId = Convert.ToInt32(row.GetCell(0)?.ToString());
            resultData.ResourceId = row.GetCell(1)?.ToString();
            resultData.HomeValue = row.GetCell(2)?.ToString();
            resultData.AwayValue = row.GetCell(3)?.ToString();
            resultData.ResultStatusID = Convert.ToInt32(row.GetCell(4)?.ToString());
            resultData.SettlementStatusID = Convert.ToInt32(row.GetCell(5)?.ToString());

            return resultData;
        }
    }
}