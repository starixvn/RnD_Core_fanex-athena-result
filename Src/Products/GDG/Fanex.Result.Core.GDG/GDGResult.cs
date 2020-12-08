namespace Fanex.Result.Core.GDG
{
    using System;
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.GDG.Models;
    using Fanex.Result.Core.GDG.Ultilities.Serializer;
    using Fanex.Result.Core.GDG.Ultilities.WebClient;

    public class GoldDiamondGamingResult : IGoldDiamondGamingResult
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

        private readonly GdgConfigs _configs;
        private readonly IWebClient _client;
        private readonly ISerializer _serializer;

        public GoldDiamondGamingResult(IWebClient client = null, ISerializer serializer = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
            _configs = GdgConfigs.Configs;
        }

        public string GetResult(string custId, string transId3rd, string gameId3rd, string language = "en-US", string siteId = null)
        {
            CheckArgument(custId, "custId");
            CheckArgument(transId3rd, "transId3rd");
            CheckArgument(gameId3rd, "gameId3rd");

            var requestData = GenerateRequestData(custId, transId3rd, gameId3rd, language, siteId);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new GdgException("Request data:" + _serializer.Serialize(requestData));
            }
            else
            {
                var responseData = _client.QueryString<Response>(_configs.Url, requestData);

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

        private Dictionary<string, string> GenerateRequestData(string custId, string transId3rd, string gameId3rd, string language, string siteId)
        {
            var languageCode = string.IsNullOrEmpty(language) || !SupportedLanguages.ContainsKey(language)
                                ? SupportedLanguages[DefaultLanguage]
                                : SupportedLanguages[language];

            var random = new Random();
            var timestampWithRandom = $"{DateTime.Now.Ticks}-{random.Next(0, int.MaxValue)}";
            var hashedPreInfo = $"{_configs.SiteId}-{custId}-{transId3rd}-{random.Next(0, int.MaxValue)}";
            var seq = $"{timestampWithRandom}-{hashedPreInfo}";

            return new Dictionary<string, string>
            {
                { "TimeStamp" ,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") },
                { "SiteId", siteId ?? _configs.SiteId },
                { "Seq" , seq },
                { "OBCustId" , custId },
                { "VendorId" , _configs.VendorId },
                { "Language" , languageCode },
                { "TransId3rd" , transId3rd },
                { "GameId3rd" , gameId3rd },
            };
        }

        private bool ValidateResponse(Response responseData, Dictionary<string, string> requestData)
        {
            if (responseData.Seq != requestData["Seq"] ||
                responseData.VendorId != requestData["VendorId"] ||
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
                        throw new ResultCoreException($"Url of GDG send request incorrectly!");
                    }
                }
                else
                {
                    throw new ResultCoreException($"Url of GDG send request incorrectly! Detail request:" +
                        $" {_serializer.Serialize(requestData)}; \n Detail response: {_serializer.Serialize(responseData)}");
                }
            }

            return true;
        }
    }
}