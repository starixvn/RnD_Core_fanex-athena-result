namespace MvcExample.Models.ResultServices
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class LeaguesResultService : IResultService<League>
    {
        public IEnumerable<League> GetResult(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<League>(criteria.GetSpParams());
            }
        }

        public IEnumerable<League> GetResultFromExcel(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}