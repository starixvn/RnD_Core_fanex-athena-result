namespace Fanex.Result.Sites.NET8.Models.ResultServices
{
    using System.Collections.Generic;

    internal interface IResultService<out T>
    {
        IEnumerable<T> GetResultFromExcel(string filePath);
    }
}