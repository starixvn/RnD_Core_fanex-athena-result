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
            resultData.Bettype = Convert.ToInt32(row.GetCell(1).ToString());
            resultData.IsHTCal = Convert.ToBoolean(Convert.ToInt16(row.GetCell(2).ToString()));
            resultData.Calculate = Convert.ToBoolean(Convert.ToInt16(row.GetCell(3).ToString()));
            resultData.LeagueId = Convert.ToInt32(row.GetCell(4)?.ToString());
            resultData.HomeId = Convert.ToInt32(row.GetCell(5)?.ToString());
            resultData.AwayId = Convert.ToInt32(row.GetCell(6)?.ToString());
            resultData.EventDate = Convert.ToDateTime(row.GetCell(7)?.ToString());
            resultData.EventStatus = row.GetCell(8)?.ToString();
            resultData.BetStatus = row.GetCell(9)?.ToString();
            resultData.LiveHomeScore = Convert.ToInt32(row.GetCell(10)?.ToString());
            resultData.LiveAwayScore = Convert.ToInt32(row.GetCell(11)?.ToString());
            resultData.FinalHomeScore = Convert.ToInt32(row.GetCell(12)?.ToString());
            resultData.FinalAwayScore = Convert.ToInt32(row.GetCell(13)?.ToString());
            resultData.MatchCode = row.GetCell(15)?.ToString();
            resultData.KickOffTime = Convert.ToDateTime(row.GetCell(16)?.ToString());
            resultData.HtHomeScore = Convert.ToInt32(row.GetCell(19)?.ToString());
            resultData.HtAwayScore = Convert.ToInt32(row.GetCell(20)?.ToString());
            resultData.GoalRecord = row.GetCell(35)?.ToString();
            resultData.BetID = "3";
            resultData.SettlementStatus = Convert.ToByte(string.IsNullOrEmpty(row.GetCell(17)?.ToString()) ? "0" : row.GetCell(17)?.ToString());
            resultData.NoHTScore = Convert.ToBoolean(Convert.ToInt16(row.GetCell(45).ToString()));
            resultData.MainType = Convert.ToByte(row.GetCell(46).ToString());
            resultData.SubType = Convert.ToInt32(row.GetCell(47).ToString());

            return resultData;
        }
    }
}