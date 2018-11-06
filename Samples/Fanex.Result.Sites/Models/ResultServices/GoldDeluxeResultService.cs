namespace MvcExample.Models.ResultServices
{
    using System.Collections.Generic;
    using Criteria;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Fanex.Result.Core.GoldDeluxe.DTO;
    using Fanex.Result.Data;

    public class GoldDeluxeResultService : IResultService<GoldDeluxe>
    {
        public IEnumerable<GoldDeluxe> GetResult(ICriteria criteria)
        {
            var result = new List<GoldDeluxe>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        result.Add(new GoldDeluxe
                        {
                            Bettype = (criteria as GetGolDeluxePopUpResultCriteria).Bettype,
                            Result = reader["TransDesc"].ToString()
                        });
                    }
                }
            }

            return result;
        }

        public IEnumerable<GoldDeluxe> GetResultFromExcel(string filePath)
            => GoldDeluxeResultExcelReader
                    .CreateInstance(filePath)
                    .GetResultData();
    }
}