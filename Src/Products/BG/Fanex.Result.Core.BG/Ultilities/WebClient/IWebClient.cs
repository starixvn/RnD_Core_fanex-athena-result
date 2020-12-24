namespace Fanex.Result.Core.BG.Ultilities.WebClient
{
    using System.Collections.Generic;

    public interface IWebClient
    {
        TOut QueryString<TOut>(string address, Dictionary<string, string> requestData, string proxyServer = "", int port = 0);
    }
}