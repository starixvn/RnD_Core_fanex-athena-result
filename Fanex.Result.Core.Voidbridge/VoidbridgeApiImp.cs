namespace Fanex.Result.Core.Voidbridge
{
    using System;
    using System.Configuration;
    using System.Net;
    using System.Runtime.Caching;
    using System.Security.Cryptography;
    using Exceptions;
    using Models;
    using Newtonsoft.Json;

    public class VoidbridgeApiImp : VoidbridgeApi
    {
        private const string APIKey = "VoidbridgeAPI";
        private const string LoginKey = "VoidbridgeLogin";
        private const string PasswordKey = "VoidbridgePassword";
        private const string SiteIdKey = "VoidbridgeSiteId";
        private const string VendorIdKey = "VoidbridgeVendorId";

        private const string ConfigCacheKey = "ConfigCacheKey";

        private Config _config;
        private string _timeStamp;
        private string _seq;

        public VoidbridgeApiImp()
        {
            _config = GetConfig();
            _timeStamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
        }

        public string GetBetHistoryUrl(string custId, string gamePlayId)
        {
            if (ValidateArguments(custId, gamePlayId))
            {
                GenerateSeqCode(custId, gamePlayId);
                var response = SendRequest(custId, gamePlayId);

                return GetUrlFromResponse(response);
            }

            return string.Empty;
        }

        private bool ValidateArguments(string custId, string gamePlayId)
        {
            if (string.IsNullOrEmpty(custId))
            {
                throw new VoidbridgeException("custId is null or empty!");
            }

            if (string.IsNullOrEmpty(gamePlayId))
            {
                throw new VoidbridgeException("gamePlayId is null or empty!");
            }

            return true;
        }

        private void GenerateSeqCode(string custId, string gamePlayId)
        {
            var random = new Random();
            var hashedTimestampWithRandom = MD5.Create($"{_timeStamp}-{random.Next(0, int.MaxValue)}");
            var hashedPreInfo = MD5.Create($"{_config.SiteId}-{custId}-{gamePlayId}");

            _seq = $"{hashedTimestampWithRandom}-{hashedPreInfo}";
        }

        private string SendRequest(string custId, string gamePlayId)
        {
            var result = string.Empty;
            var jsonData = CreateJsonData(custId, gamePlayId);

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                result = client.UploadString(_config.VoidbridgeAPI, jsonData);
            }

            return result;
        }

        private string CreateJsonData(string custId, string gamePlayId)
        {
            var request = CreateRequestObject(custId, gamePlayId);

            return JsonConvert.SerializeObject(request);
        }

        private Request CreateRequestObject(string custId, string gamePlayId)
        {
            var request = new Request();

            request.TimeStamp = _timeStamp;
            request.Auth = CreateAuthObject();
            request.Call = CreateCallObject(custId, gamePlayId);

            return request;
        }

        private Auth CreateAuthObject()
        {
            var auth = new Auth();

            auth.Login = _config.Login;
            auth.Password = _config.Password;
            auth.SiteId = _config.SiteId;

            return auth;
        }

        private Call CreateCallObject(string custId, string gamePlayId)
        {
            var call = new Call();

            call.Seq = _seq;
            call.OBCustId = custId;
            call.GamePlayId = gamePlayId;
            call.VendorId = _config.VendorId;

            return call;
        }

        private Config GetConfig()
        {
            ObjectCache cache = MemoryCache.Default;

            var config = (Config)cache[ConfigCacheKey];
            if (config == null)
            {
                config = new Config();

                config.VoidbridgeAPI = ConfigurationManager.AppSettings[APIKey];
                config.Login = ConfigurationManager.AppSettings[LoginKey];
                config.Password = ConfigurationManager.AppSettings[PasswordKey];
                config.SiteId = ConfigurationManager.AppSettings[SiteIdKey];
                config.VendorId = ConfigurationManager.AppSettings[VendorIdKey];

                ValidateConfig(config);
            }

            return config;
        }

        private void ValidateConfig(Config config)
        {
            if (string.IsNullOrEmpty(config.VoidbridgeAPI))
            {
                throw new VoidbridgeException($"Wrong Config! Not found {APIKey} key !");
            }

            if (string.IsNullOrEmpty(config.Login))
            {
                throw new VoidbridgeException($"Wrong Config! Not found {LoginKey} key !");
            }

            if (string.IsNullOrEmpty(config.Password))
            {
                throw new VoidbridgeException($"Wrong Config! Not found {PasswordKey} key !");
            }

            if (string.IsNullOrEmpty(config.SiteId))
            {
                throw new VoidbridgeException($"Wrong Config! Not found {SiteIdKey} key !");
            }

            if (string.IsNullOrEmpty(config.VendorId))
            {
                throw new VoidbridgeException($"Wrong Config! Not found {VendorIdKey} key !");
            }
        }

        private string GetUrlFromResponse(string response)
        {
            var responseObject = DeserializeResponse(response);
            var url = ValidateResponse(responseObject)
                        ? responseObject.Result.Url
                        : string.Empty;

            return url;
        }

        private Response DeserializeResponse(string response)
        {
            return JsonConvert.DeserializeObject<Response>(response);
        }

        private bool ValidateResponse(Response responseObject)
        {
            return responseObject.TimeStamp == _timeStamp && responseObject.Result.Seq == _seq;
        }

        public string RenderBetHistory(string custId, string gamePlayId)
        {
            throw new NotImplementedException();
        }
    }
}