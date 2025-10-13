using Fanex.Result.Sites.NET8.Models;

namespace Fanex.Result.Sites.NET8.Models;

public class ThirdPartyResult : IThirdPartyResult
{
    public string GetResultPOST(ResultRequest request)
    {
        // Placeholder implementation
        return "http://example.com/result";
    }

    public string GetResultGET(ResultRequest request)
    {
        // Placeholder implementation
        return "http://example.com/result";
    }
}
