namespace Fanex.Result.Core.VirtualGames
{
    public interface IVirtualGamesResult
    {
        string GetResult(string platform, string language = "en-US");

        string GetDetailResult(string gameId, string platform, string language = "en-US");
    }
}