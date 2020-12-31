namespace Fanex.Result.Core.Happy5LinkedGames
{
    public interface IHappy5LinkedGamesResult
    {
        string GetResult(string custId, string transId3rd, string gameId3rd, string language = "en-US", string siteId = null);
    }
}