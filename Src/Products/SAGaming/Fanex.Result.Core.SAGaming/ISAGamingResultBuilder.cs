namespace Fanex.Result.Core.SAGaming
{
    public interface ISAGamingResultBuilder
    {
        string Render(string transDesc, string gameId3rd, string betType, string languageCode = "en-US");
    }
}