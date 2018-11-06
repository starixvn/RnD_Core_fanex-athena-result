namespace Fanex.Result.Core.AllBetCasino
{
    public interface IAllbetResultBuilder
    {
        string Render(string transDesc, string gameId3rd, string betType, string languageCode = "en-US");
    }
}