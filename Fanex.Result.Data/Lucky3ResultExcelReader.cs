namespace Fanex.Result.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Lucky3.DTO;
    using NPOI.SS.UserModel;

    public class Lucky3ResultExcelReader : ResultExcelReader<Lucky3>
    {
        public Lucky3ResultExcelReader(string filePath) : base(filePath)
        {
        }

        public static Lucky3ResultExcelReader CreateInstance(string filePath)
        {
            return new Lucky3ResultExcelReader(filePath);
        }

        protected override IEnumerable<Lucky3> GetResultData(ISheet sheet)
        {
            var resultData = new List<Lucky3>();

            foreach (IRow row in sheet)
            {
                var matchId = Convert.ToInt32(row.GetCell(0)?.ToString());
                var existedMatch = resultData.SingleOrDefault(i => i.MatchId == matchId);

                if (existedMatch != null)
                {
                    existedMatch.Seq.Add(Convert.ToInt32(row.GetCell(1)?.ToString()));
                    existedMatch.Ball.Add(Convert.ToInt32(row.GetCell(2)?.ToString()));
                }
                else
                {
                    resultData.Add(MapResultData(row));
                }
            }

            return resultData;
        }

        protected override Lucky3 MapResultData(IRow row)
        {
            var resultData = new Lucky3();

            resultData.MatchId = Convert.ToInt32(row.GetCell(0)?.ToString());
            resultData.Seq = new List<int> { Convert.ToInt32(row.GetCell(1)?.ToString()) };
            resultData.Ball = new List<int> { Convert.ToInt32(row.GetCell(2)?.ToString()) };
            resultData.KickOffTime = Convert.ToDateTime(row.GetCell(4)?.ToString());
            resultData.EventDate = Convert.ToDateTime(row.GetCell(5)?.ToString());
            resultData.EventStatus = row.GetCell(6)?.ToString();
            resultData.MatchCode = row.GetCell(7)?.ToString();
            resultData.LeagueId = Convert.ToInt32(row.GetCell(8)?.ToString());
            resultData.LiveHomeScore = Convert.ToInt32(row.GetCell(9)?.ToString());
            resultData.LiveAwayScore = Convert.ToInt32(row.GetCell(10)?.ToString());
            resultData.FinalHomeScore = Convert.ToInt32(row.GetCell(11)?.ToString());
            resultData.SettlementStatus = Convert.ToByte(row.GetCell(12)?.ToString());
            resultData.BetStatus = row.GetCell(13)?.ToString();

            return resultData;
        }
    }
}