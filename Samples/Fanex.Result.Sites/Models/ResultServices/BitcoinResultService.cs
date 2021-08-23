namespace MvcExample.Models.ResultServices
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Fanex.Data.Repository;
    using Fanex.Result.Core.Bitcoin.DTO;
    using Fanex.Result.Data;

    public class BitcoinResultService : IResultService<Bitcoin>
    {
        public IEnumerable<Bitcoin> GetResultFromExcel(string filePath)
        {
            var bitcoins = BitcoinResultExcelReader
                .CreateInstance(filePath)
                .GetResultData();
            var details = BitcoinDetailResultExcelReader
                .CreateInstance(filePath.Replace("Bitcoin", "BitcoinDetail"))
                .GetResultData().ToList();
            foreach (var item in bitcoins)
            {
                item.GameResults = details.Where(i => i.Match3rdId == item.Match3rdId).ToList();
            }

            return bitcoins;
        }

        public IEnumerable<Bitcoin> GetResult(ICriteria criteria)
        {
            return new List<Bitcoin>();
        }
        
    }
}