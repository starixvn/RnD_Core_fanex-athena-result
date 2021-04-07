namespace Fanex.Result.Core.Base.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    public interface IWebClient
    {
        TOut UploadString<TIn, TOut>(string address, TIn requestData, string proxyServer = "", int port = 0);
        TOut QueryString<TOut>(string address, Dictionary<string, string> requestData, string proxyServer = "", int port = 0);
    }

    public class JsonWebClient : IWebClient, IDisposable
    {
        private readonly WebClient _client;
        private readonly ISerializer _serializer;

        public JsonWebClient(ISerializer serializer)
        {
            _client = new WebClient();
            
            _serializer = serializer ?? new JsonSerializer();
        }

        public TOut UploadString<TIn, TOut>(string address, TIn requestData, string proxyServer = "", int port = 0)
        {
            if (!string.IsNullOrEmpty(proxyServer) && port != 0)
            {
                var proxy = new WebProxy(proxyServer, port);
                _client.Proxy = proxy;
            }

            var serializedRequestData = _serializer.Serialize(requestData);
            _client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var response = _client.UploadString(address, "POST", serializedRequestData);

            return _serializer.Deserialize<TOut>(response);
        }

        public TOut QueryString<TOut>(string address, Dictionary<string, string> requestData, string proxyServer = "", int port = 0)
        {
            if (!string.IsNullOrEmpty(proxyServer) && port != 0)
            {
                var proxy = new WebProxy(proxyServer, port);
                _client.Proxy = proxy;
            }

            foreach (var item in requestData)
            {
                _client.QueryString.Add(item.Key, item.Value);
            }

            var response = _client.DownloadString(address);

            return _serializer.Deserialize<TOut>(response);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_client != null)
            {
                _client.Dispose();
            }
        }
    }
}
