namespace MvcExample.Models.ResultServices
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Fanex.Result.Core.LiveCasino.DTO;

    public class LiveCasinoResultService : IResultService<LiveCasino>
    {
        public IEnumerable<LiveCasino> GetResult(ICriteria criteria)
        {
            var result = new List<LiveCasino>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        result.Add(BindData(reader));
                    }
                }
            }

            return result;
        }

        private static LiveCasino BindData(IDataReader reader)
        {
            return new LiveCasino
            {
                ShowTime = reader["showtime"].ToString(),
                LeagueId = int.Parse(reader["leagueid"].ToString()),
                EventStatus = reader["eventstatus"].ToString(),
                FinalAwayScore =
                     reader["finalawayscore"] != DBNull.Value ? int.Parse(reader["finalawayscore"].ToString()) : new int?(),
                FinalHomeScore =
                     reader["finalhomescore"] != DBNull.Value ? int.Parse(reader["finalhomescore"].ToString()) : new int?(),
                EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                IsPending = bool.Parse(reader["IsPending"].ToString()),
                KickOffTime = DateTime.Parse(reader["kickofftime"].ToString()),
                IsHTCal = Convert.ToBoolean(reader["IsHTCal"]),
                Calculate = Convert.ToBoolean(reader["Calculate"]),
                BetStatus = reader["betstatus"] != DBNull.Value ? reader["betstatus"].ToString() : string.Empty,
                Bettype = int.Parse(reader["homeid2"].ToString())
            };
        }

        public IEnumerable<LiveCasino> GetResultFromExcel(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}