namespace Fanex.Result.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Result.Core.NumberGame.DTO;
    using NPOI.SS.UserModel;

    public class NumberGameResultExcelReader : ResultExcelReader<NumberGame>
    {
        public NumberGameResultExcelReader(string filePath) : base(filePath)
        {
        }

        public static NumberGameResultExcelReader CreateInstance(string filePath)
        {
            return new NumberGameResultExcelReader(filePath);
        }

        protected override IEnumerable<NumberGame> GetResultData(ISheet sheet)
        {
            var resultData = new List<NumberGame>();

            foreach (IRow row in sheet)
            {
                var matchId = Convert.ToInt32(row.GetCell(0)?.ToString());
                var existedMatch = resultData.SingleOrDefault(i => i.MatchId == matchId);

                if (existedMatch != null)
                {
                    existedMatch.Seg.Add(Convert.ToInt32(row.GetCell(1)?.ToString()));
                    existedMatch.Ball.Add(Convert.ToInt32(row.GetCell(2)?.ToString()));
                }
                else
                {
                    resultData.Add(MapResultData(row));
                }
            }

            return resultData;
        }

        protected override NumberGame MapResultData(IRow row)
        {
            var resultData = new NumberGame();
            var seg = row.GetCell(1).ToString();
            var ball = row.GetCell(2).ToString();
            if (seg == "")
            {
                resultData.Seg = new List<int>();
                resultData.Ball = new List<int>();
            }
            else
            {
                resultData.Seg = new List<int> { Convert.ToInt32(seg) };
                resultData.Ball = new List<int> { Convert.ToInt32(ball) };
            }

            resultData.MatchId = Convert.ToInt32(row.GetCell(0)?.ToString());
            resultData.KickOffTime = Convert.ToDateTime(row.GetCell(4)?.ToString());
            resultData.EventDate = Convert.ToDateTime(row.GetCell(5)?.ToString());
            resultData.EventStatus = row.GetCell(6)?.ToString();
            resultData.MatchCode = row.GetCell(7)?.ToString();
            resultData.LeagueId = Convert.ToInt32(row.GetCell(8)?.ToString());
            resultData.IsPending = row.GetCell(14)?.ToString() == "1";

            return resultData;
        }
    }
}