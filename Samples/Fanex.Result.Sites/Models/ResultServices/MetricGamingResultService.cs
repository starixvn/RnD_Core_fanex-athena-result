namespace MvcExample.Models.ResultServices
{
    using System;
    using System.Collections.Generic;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Fanex.Result.Core.MetricGaming.DTO;

    public class MetricGamingResultService : IResultService<MetricGaming>
    {
        public IEnumerable<MetricGaming> GetResult(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<MetricGaming>(criteria.GetSpParams());
            }
        }

        public IEnumerable<MetricGaming> GetResultFromExcel(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}