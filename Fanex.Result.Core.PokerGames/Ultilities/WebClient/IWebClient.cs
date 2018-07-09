namespace Fanex.Result.Core.PokerGames.Ultilities.WebClient
{
    public interface IWebClient
    {
        TOut UploadString<TIn, TOut>(string address, TIn requestData);
    }
}