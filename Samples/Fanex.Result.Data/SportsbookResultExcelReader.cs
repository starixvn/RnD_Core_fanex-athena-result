namespace Fanex.Result.Data
{
    using System;
    using Core.Sportsbook.DTO;
    using NPOI.SS.UserModel;

    public class SportsbookResultExcelReader : ResultExcelReader<Sportsbook>
    {
        public SportsbookResultExcelReader(string filePath) : base(filePath)
        {
        }

        public static SportsbookResultExcelReader CreateInstance(string filePath)
        {
            return new SportsbookResultExcelReader(filePath);
        }

        protected override Sportsbook MapResultData(IRow row)
        {
            var resultData = new Sportsbook();

            resultData.MatchId = Convert.ToInt32(row.GetCell(0)?.ToString());
            resultData.LeagueId = Convert.ToInt32(row.GetCell(1)?.ToString());
            resultData.HomeId = Convert.ToInt32(row.GetCell(2)?.ToString());
            resultData.AwayId = Convert.ToInt32(row.GetCell(3)?.ToString());
            resultData.EventDate = Convert.ToDateTime(row.GetCell(4)?.ToString());
            resultData.EventStatus = row.GetCell(5)?.ToString();
            resultData.BetStatus = row.GetCell(6)?.ToString();
            resultData.LiveHomeScore = Convert.ToInt32(row.GetCell(7)?.ToString());
            resultData.LiveAwayScore = Convert.ToInt32(row.GetCell(8)?.ToString());
            resultData.FinalHomeScore = Convert.ToInt32(row.GetCell(9)?.ToString());
            resultData.FinalAwayScore = Convert.ToInt32(row.GetCell(10)?.ToString());
            resultData.MatchCode = row.GetCell(12)?.ToString();
            resultData.KickOffTime = Convert.ToDateTime(row.GetCell(13)?.ToString());
            resultData.HtHomeScore = Convert.ToInt32(row.GetCell(16)?.ToString());
            resultData.HtAwayScore = Convert.ToInt32(row.GetCell(17)?.ToString());
            resultData.GoalRecord = row.GetCell(32)?.ToString();
            resultData.BetID = "3";
            resultData.SettlementStatus = Convert.ToByte(string.IsNullOrEmpty(row.GetCell(14)?.ToString()) ? "0" : row.GetCell(14)?.ToString());

            return resultData;
        }
    }
}