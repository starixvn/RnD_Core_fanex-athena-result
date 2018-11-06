namespace Fanex.Result.Core.PokerGames
{
    using Fanex.Result.Core.PokerGames.Models;

    public interface IPokerGamesResult
    {
        string GetResult(string custId, string gameTypeId, string transId3rd, ViewerLevel viewerLevel, string language = "en-US", int siteId = 0);
    }
}
