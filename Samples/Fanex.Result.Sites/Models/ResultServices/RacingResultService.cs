namespace MvcExample.Models.ResultServices
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Fanex.Result.Core.Racing.DTO;

    public class RacingResultService : IResultService<Racing>
    {
        public IEnumerable<Racing> GetResult(ICriteria criteria)
        {
            var result = new List<Racing>();

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

        private static Racing BindData(IDataReader reader)
        {
            return new Racing
            {
                LeagueId = int.Parse(reader["meetingid"].ToString()),
                EngineId = reader["HorseId"].ToString(),
                EngineNo = reader["HorseNo"].ToString(),
                IsFavor = reader["IsFavor"] != DBNull.Value && reader["IsFavor"].ToString().Equals("1"),
                IsSCR = reader["IsSCR"] != DBNull.Value && reader["IsSCR"].ToString().Equals("1"),
                Place = int.Parse(reader["place"].ToString()),
                PlaceOdds = decimal.Parse(reader["placeodds"].ToString()),
                WinOdds = decimal.Parse(reader["odds"].ToString()),
                RaceName = reader["racename"] != DBNull.Value ? int.Parse(reader["racename"].ToString()) : 0,
                KickOffTime = reader["kickofftime"] != DBNull.Value ? DateTime.Parse(reader["kickofftime"].ToString()) : DateTime.MinValue,
                Favourite = reader["IsFavor"] != DBNull.Value ? reader["IsFavor"].ToString() : string.Empty,
                RowCount = int.Parse(reader["rowcount"].ToString()),
                SCROfMatch = reader["SCR"].ToString(),
                TFOfMatch = reader["TF"].ToString(),
                Status = reader["status"].ToString()
            };
        }

        public IEnumerable<Racing> GetResultFromExcel(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}