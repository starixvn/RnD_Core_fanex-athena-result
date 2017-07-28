namespace MvcExample.Models.ResultServices
{
    using System.Collections.Generic;
    using Fanex.Data.Repository;

    internal interface IResultService<out T>
    {
        IEnumerable<T> GetResult(ICriteria criteria);

        IEnumerable<T> GetResultFromExcel(string filePath);
    }
}