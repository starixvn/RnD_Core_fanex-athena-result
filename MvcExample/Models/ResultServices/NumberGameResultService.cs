namespace MvcExample.Models.ResultServices
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Fanex.Result.Core.NumberGame.DTO;
    using Fanex.Result.Data;

    public class NumberGameResultService : IResultService<NumberGame>
    {
        public IEnumerable<NumberGame> GetResult(ICriteria criteria)
        {
            var result = new List<NumberGame>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        BindData(result, reader);
                    }
                }
            }

            return result;
        }

        private static void BindData(List<NumberGame> result, IDataReader reader)
        {
            var matchId = int.Parse(reader["matchid"].ToString());
            var existMatch = result.SingleOrDefault(i => i.MatchId == matchId);

            if (existMatch != null)
            {
                existMatch.Seg.Add(int.Parse(reader["seq"].ToString()));
                existMatch.Ball.Add(int.Parse(reader["number"].ToString()));
            }
            else
            {
                result.Add(new NumberGame
                {
                    LeagueId = int.Parse(reader["leagueid"].ToString()),
                    MatchId = int.Parse(reader["matchid"].ToString()),
                    EventStatus = reader["eventstatus"].ToString(),
                    EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                    KickOffTime = DateTime.Parse(reader["kickofftime"].ToString()),
                    MatchCode = reader["matchcode"].ToString(),
                    Seg = new List<int> { int.Parse(reader["seq"].ToString()) },
                    Ball = new List<int> { int.Parse(reader["number"].ToString()) },
                    SettlementStatus = byte.Parse(reader["settlementstatus"].ToString()),
                    BetStatus = reader["betstatus"].ToString()
                });
            }
        }

        public IEnumerable<NumberGame> GetResultFromExcel(string filePath)
        {
            return NumberGameResultExcelReader
               .CreateInstance(filePath)
               .GetResultData();
        }
    }
}