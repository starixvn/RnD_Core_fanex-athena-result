using System.Collections.Generic;

namespace Fanex.Result.Core.SportsLottery.Ultilities.WebClient
{
    public interface IWebClient
    {
        TOut QueryString<TOut>(string address, Dictionary<string, string> requestData, string proxyId= "", int proxyPort = 0);
        TOut UploadString<TIn, TOut>(string address, TIn requestData, string proxyId = "", int proxyPort = 0);
    }
}