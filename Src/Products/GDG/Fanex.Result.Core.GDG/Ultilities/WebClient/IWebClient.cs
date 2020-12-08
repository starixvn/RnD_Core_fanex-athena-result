namespace Fanex.Result.Core.GDG.Ultilities.WebClient
{
    using System.Collections.Generic;

    public interface IWebClient
    {
        TOut QueryString<TOut>(string address, Dictionary<string, string> requestData);
    }
}