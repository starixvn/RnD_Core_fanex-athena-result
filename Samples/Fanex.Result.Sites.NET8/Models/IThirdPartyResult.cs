using Fanex.Result.Sites.NET8.Models;

namespace Fanex.Result.Sites.NET8.Models;

public interface IThirdPartyResult
{
    string GetResultPOST(ResultRequest request);
    string GetResultGET(ResultRequest request);
}
