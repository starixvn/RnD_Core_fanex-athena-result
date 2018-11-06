namespace MvcExample.Models.ResultServices
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Fanex.Result.Core.Happy5.DTO;
    using Fanex.Result.Data;

    public class Happy5ResultService : IResultService<Happy5>
    {
        public IEnumerable<Happy5> GetResult(ICriteria criteria)
        {
            var result = new List<Happy5>();

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

        public IEnumerable<Happy5> GetResultFromExcel(string filePath)
        {
            return Happy5ResultExcelReader
                .CreateInstance(filePath)
                .GetResultData();
        }

#pragma warning disable S1541 // Methods and properties should not be too complex

        private static void BindData(List<Happy5> result, IDataReader reader)
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
                result.Add(new Happy5
                {
                    LeagueId = int.Parse(reader["leagueid"].ToString()),
                    MatchId = int.Parse(reader["matchid"].ToString()),
                    EventStatus = reader["eventstatus"].ToString(),
                    EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                    KickOffTime = DateTime.Parse(reader["kickofftime"].ToString()),
                    MatchCode = reader["matchcode"].ToString(),
                    Seq = new List<int> { int.Parse(reader["seq"].ToString()) },
                    Ball = new List<int> { int.Parse(reader["number"].ToString()) },
                    LiveHomeScore = int.Parse(string.IsNullOrEmpty(reader["livehomescore"]?.ToString()) ? "0" : reader["livehomescore"]?.ToString()),
                    LiveAwayScore = int.Parse(string.IsNullOrEmpty(reader["liveawayscore"]?.ToString()) ? "0" : reader["liveawayscore"]?.ToString()),
                    FinalHomeScore = int.Parse(string.IsNullOrEmpty(reader["finalhomescore"]?.ToString()) ? "0" : reader["finalhomescore"]?.ToString()),
                });
            }
        }

#pragma warning restore S1541 // Methods and properties should not be too complex
    }
}