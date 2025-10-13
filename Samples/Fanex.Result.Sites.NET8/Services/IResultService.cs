using Fanex.Result.Sites.NET8.Models;

namespace Fanex.Result.Sites.NET8.Services;

public interface IResultService
{
    Task<string> GetResultAsync(ResultRequest request);
    Task<string> GetResultPOSTAsync(ResultRequest request);
    Task<string> GetResultGETAsync(ResultRequest request);
}
