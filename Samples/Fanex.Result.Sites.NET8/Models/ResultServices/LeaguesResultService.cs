namespace Fanex.Result.Sites.NET8.Models.ResultServices
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class LeaguesResultService : IResultService<League>
    {

        public IEnumerable<League> GetResultFromExcel(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}