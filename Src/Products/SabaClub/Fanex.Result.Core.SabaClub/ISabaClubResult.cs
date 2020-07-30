namespace Fanex.Result.Core.SabaClub
{
    public interface ISabaClubResult
    {
        string GetResult(string custId, string gameRoundId, string gameId, string siteId, string language = "en-US");
    }
}