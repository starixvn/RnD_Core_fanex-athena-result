namespace Fanex.Result.Core.AllBetCasino
{
    public interface IBuilderService
    {
        string Render(string transDesc, string betCheck, string betType, string languageCode = "en-US");
    }
}