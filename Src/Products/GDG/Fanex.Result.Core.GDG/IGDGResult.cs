namespace Fanex.Result.Core.GDG
{
    public interface IGoldDiamondGamingResult
    {
        string GetResult(string custId, string transId3rd, string gameId3rd, string language = "en-US", string siteId = null);
    }
}