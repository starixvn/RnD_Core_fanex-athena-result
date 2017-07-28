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

            resultData.BetStatus = row.GetCell(1)?.ToString();
            resultData.LeagueId = Convert.ToInt32(row.GetCell(2)?.ToString());
            resultData.MatchId = Convert.ToInt32(row.GetCell(3)?.ToString());
            resultData.Color = row.GetCell(4)?.ToString();
            resultData.MatchCode = row.GetCell(5)?.ToString();
            resultData.EventDate = Convert.ToDateTime(row.GetCell(6)?.ToString());
            resultData.KickOffTime = Convert.ToDateTime(row.GetCell(7)?.ToString());
            resultData.HomeId = Convert.ToInt32(row.GetCell(8)?.ToString());
            resultData.AwayId = Convert.ToInt32(row.GetCell(9)?.ToString());
            resultData.HtHomeScore = Convert.ToInt32(row.GetCell(10)?.ToString());
            resultData.HtAwayScore = Convert.ToInt32(row.GetCell(11)?.ToString());
            resultData.FinalHomeScore = Convert.ToInt32(row.GetCell(12)?.ToString());
            resultData.FinalAwayScore = Convert.ToInt32(row.GetCell(13)?.ToString());
            resultData.EventStatus = row.GetCell(14)?.ToString();
            resultData.IsNeutral = ConvertStringToBoolean(row.GetCell(17)?.ToString());
            resultData.GoalRecord = row.GetCell(18)?.ToString();
            resultData.IsHTCal = ConvertStringToBoolean(row.GetCell(19)?.ToString());
            resultData.Calculate = ConvertStringToBoolean(row.GetCell(20)?.ToString());
            resultData.IsPending = ConvertStringToBoolean(row.GetCell(21)?.ToString());

            return resultData;
        }
    }
}