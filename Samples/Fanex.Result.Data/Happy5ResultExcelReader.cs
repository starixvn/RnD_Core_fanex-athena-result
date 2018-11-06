namespace Fanex.Result.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Result.Core.Happy5.DTO;
    using NPOI.SS.UserModel;

    public class Happy5ResultExcelReader : ResultExcelReader<Happy5>
    {
        public Happy5ResultExcelReader(string filePath) : base(filePath)
        {
        }

        public static Happy5ResultExcelReader CreateInstance(string filePath)
        {
            return new Happy5ResultExcelReader(filePath);
        }

        protected override IEnumerable<Happy5> GetResultData(ISheet sheet)
        {
            var resultData = new List<Happy5>();

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

#pragma warning disable S1541 // Methods and properties should not be too complex

        protected override Happy5 MapResultData(IRow row)
        {
            return new Happy5
            {
                MatchId = Convert.ToInt32(row.GetCell(0)?.ToString()),
                Seq = new List<int> { Convert.ToInt32(row.GetCell(1)?.ToString()) },
                Ball = new List<int> { Convert.ToInt32(row.GetCell(2)?.ToString()) },
                KickOffTime = Convert.ToDateTime(row.GetCell(4)?.ToString()),
                EventDate = Convert.ToDateTime(row.GetCell(5)?.ToString()),
                EventStatus = row.GetCell(6)?.ToString(),
                MatchCode = row.GetCell(7)?.ToString(),
                LeagueId = Convert.ToInt32(row.GetCell(8)?.ToString()),
                LiveHomeScore = Convert.ToInt32(row.GetCell(9)?.ToString()),
                LiveAwayScore = Convert.ToInt32(row.GetCell(10)?.ToString()),
                FinalHomeScore = Convert.ToInt32(row.GetCell(11)?.ToString()),
                IsPending = row.GetCell(14)?.ToString() == "1"
            };
        }

#pragma warning restore S1541 // Methods and properties should not be too complex
    }
}