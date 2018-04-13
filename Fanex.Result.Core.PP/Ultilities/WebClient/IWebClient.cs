namespace Fanex.Result.Core.PP.Ultilities.WebClient
{
    public interface IWebClient
    {
        TOut UploadString<TIn, TOut>(string address, TIn requestData);
    }
}