namespace Fanex.Result.Core.AllBetCasino
{
    public interface IAllbetResultBuilder
    {
        string Render(string transDesc, string matchCode, string betType, string languageCode = "en-US");
    }
}