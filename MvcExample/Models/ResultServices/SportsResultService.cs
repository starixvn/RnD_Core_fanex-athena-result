namespace MvcExample.Models.ResultServices
{
    using System;
    using System.Collections.Generic;
    using DTO;
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class SportsResultService : IResultService<Sport>
    {
        public IEnumerable<Sport> GetResult(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                return db.Query<Sport>(criteria.GetSpParams());
            }
        }

        public IEnumerable<Sport> GetResultFromExcel(string filePath)
        {
            return new List<Sport>
            {
                new Sport { Id = 1, Name = "Soccer" },
                new Sport { Id = 160, Name = "Lucky3" },
                new Sport { Id = 193, Name = "VirtualBasketball" }
            };
        }
    }
}