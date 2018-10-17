namespace MvcExample.Models.ResultServices
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Fanex.Result.Core.Lucky3.DTO;
    using Fanex.Result.Data;

    public class Lucky3ResultService : IResultService<Lucky3>
    {
        public IEnumerable<Lucky3> GetResult(ICriteria criteria)
        {
            var result = new List<Lucky3>();

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

        public IEnumerable<Lucky3> GetResultFromExcel(string filePath)
        {
            return Lucky3ResultExcelReader
                .CreateInstance(filePath)
                .GetResultData();
        }

        private static void BindData(List<Lucky3> result, IDataReader reader)
        {
            var matchId = int.Parse(reader["matchid"].ToString());
            var existMatch = result.SingleOrDefault(i => i.MatchId == matchId);

            if (existMatch != null)
            {
                existMatch.Seq.Add(int.Parse(reader["seq"].ToString()));
                existMatch.Ball.Add(int.Parse(reader["number"].ToString()));
            }
            else
            {
                result.Add(new Lucky3
                {
                    LeagueId = int.Parse(reader["leagueid"].ToString()),
                    MatchId = int.Parse(reader["matchid"].ToString()),
                    EventStatus = reader["eventstatus"].ToString(),
                    EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                    KickOffTime = DateTime.Parse(reader["kickofftime"].ToString()),
                    MatchCode = reader["matchcode"].ToString(),
                    Seq = new List<int> { int.Parse(reader["seq"].ToString()) },
                    Ball = new List<int> { int.Parse(reader["number"].ToString()) },
                    LiveHomeScore = int.Parse(reader["livehomescore"]?.ToString()),
                    LiveAwayScore = int.Parse(reader["liveawayscore"]?.ToString()),
                    FinalHomeScore = int.Parse(reader["finalhomescore"].ToString())
                });
            }
        }
    }
}