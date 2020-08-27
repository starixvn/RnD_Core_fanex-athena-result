namespace Fanex.Result.Core.SabaClub
{
    public interface ISabaClubResult
    {
        string GetResult(string custId, string gameRoundId, string gameId, string siteId, int sportType, string language = "en-US");
    }
}