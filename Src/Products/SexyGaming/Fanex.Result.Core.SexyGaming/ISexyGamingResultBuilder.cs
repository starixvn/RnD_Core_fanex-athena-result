namespace Fanex.Result.Core.SexyGaming
{
    public interface ISexyGamingResultBuilder
    {
        string Render(string transDesc, string gameId3rd, int betType, string languageCode = "en-US");
    }
}