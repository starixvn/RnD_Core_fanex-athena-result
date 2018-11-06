namespace MvcExample.Models.ResultServices
{
    using System;
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Fanex.Result.Core.Sportsbook.DTO;
    using Fanex.Result.Data;

    public class OutRightResultService : IResultService<Outright>
    {
        public IEnumerable<Outright> GetResult(ICriteria criteria)
        {
            var result = new List<Outright>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        result.Add(new Outright
                        {
                            EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                            SportType = int.Parse(reader["sporttype"].ToString()),
                            LeagueId = int.Parse(reader["leagueid"].ToString()),
                            TeamId = int.Parse(reader["teamid"].ToString()),
                            Status = reader["status"].ToString()
                        });
                    }
                }
            }

            return result;
        }

        public IEnumerable<Outright> GetResultFromExcel(string filePath)
        {
            return OutrightResultExcelReader
               .CreateInstance(filePath)
               .GetResultData();
        }
    }
}