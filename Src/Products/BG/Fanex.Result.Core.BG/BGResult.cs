namespace Fanex.Result.Core.BG
{
    using System;
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.BG.Models;
    using Fanex.Result.Core.BG.Ultilities.Serializer;
    using Fanex.Result.Core.BG.Ultilities.WebClient;

    public interface IBigGamingResult
    {
        string GetResult(string custId, string transId3rd, string currency, string siteId, string language = "en-US");
    }

    public class BigGamingResult : IBigGamingResult
    {
        private const string DefaultLanguage = "en-US";

        private static readonly Dictionary<string, string> SupportedLanguages = new Dictionary<string, string>
        {
            { "en-US", "en" },
            { "zh-CN", "cs" },
            { "zh-TW", "ch" },
            { "zh-Hans", "zhcn" },
            { "th-TH", "th" },
            { "vi-VN", "vn" },
            { "id-ID", "id" }
        };

        private readonly BGConfigs _configs;
        private readonly IWebClient _client;
        private readonly ISerializer _serializer;

        public BigGamingResult(IWebClient client = null, ISerializer serializer = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
            _configs = BGConfigs.Configs;
        }

        public string GetResult(string custId, string transId3rd, string currency, string siteId, string language = "en-US")
        {
            CheckArgument(custId, "custId");
            CheckArgument(transId3rd, "transId3rd");
            CheckArgument(currency, "currency");
            CheckArgument(siteId, "siteId");

            var requestData = GenerateRequestData(custId, transId3rd, currency, language, siteId);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new BGException("Request data:" + _serializer.Serialize(requestData));
            }
            else
            {
                var responseData = _client.QueryString<Response>(_configs.Url, requestData, _configs.ProxyIPServer, _configs.ProxyPortServer);

                if (ValidateResponse(responseData, requestData))
                {
                    return !string.IsNullOrWhiteSpace(responseData.Url) ?
                        responseData.Url.Replace("http:", string.Empty) :
                        string.Empty;
                }
            }

            return string.Empty;
        }

        private static void CheckArgument(string arg, string argName)
        {
            if (string.IsNullOrEmpty(arg))
            {
                throw new ResultCoreException($"{argName} can not be null or emtpy");
            }
        }

        private Dictionary<string, string> GenerateRequestData(string custId, string transId3rd, string currency, string language, string siteId)
        {
            var languageCode = string.IsNullOrEmpty(language) || !SupportedLanguages.ContainsKey(language)
                                ? SupportedLanguages[DefaultLanguage]
                                : SupportedLanguages[language];

            var random = new Random();
            var timestampWithRandom = $"{DateTime.Now.Ticks}-{random.Next(0, int.MaxValue)}";
            var hashedPreInfo = $"{currency}-{custId}-{transId3rd}-{random.Next(0, int.MaxValue)}";
            var seq = $"{timestampWithRandom}-{hashedPreInfo}";

            return new Dictionary<string, string>
            {
                { "TimeStamp" ,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") },
                { "SiteId", siteId },
                { "Seq" , seq },
                { "ObCustId" , custId },
                { "VendorId" , _configs.VendorId },
                { "Language" , languageCode },
                { "TranId3rd" , transId3rd },
                { "CurrencyId" , currency },
            };
        }

        private bool ValidateResponse(Response responseData, Dictionary<string, string> requestData)
        {
            if (responseData.Seq != requestData["Seq"] ||
                responseData.ErrorCode != "0")
            {
                if ((DebugMode)_configs.DebugMode == DebugMode.None)
                {
                    if (responseData.ErrorCode != "0")
                    {
                        throw new ResultCoreException(responseData.ErrorMsg);
                    }
                    else
                    {
                        throw new ResultCoreException($"Url of BG send request incorrectly!");
                    }
                }
                else
                {
                    throw new ResultCoreException($"Url of BG send request incorrectly! Detail request:" +
                        $" {_serializer.Serialize(requestData)}; \n Detail response: {_serializer.Serialize(responseData)}");
                }
            }

            return true;
        }
    }
}