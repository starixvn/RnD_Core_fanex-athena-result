namespace Fanex.Result.Core.LinkedGames
{
    using System;
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.LinkedGames.Models;
    using Fanex.Result.Core.LinkedGames.Ultilities.Serializer;
    using Fanex.Result.Core.LinkedGames.Ultilities.WebClient;

    public interface ILinkedGamesResult
    {
        string GetResult(string custId, string transId3rd, bool isDesktop, string language = "en-US");
    }

    public class LinkedGamesResult : ILinkedGamesResult
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

        private readonly LinkedGamesConfigs _configs;
        private readonly IWebClient _client;
        private readonly ISerializer _serializer;

        public LinkedGamesResult() : this(null, null)
        {
        }

        protected LinkedGamesResult(IWebClient client = null, ISerializer serializer = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
            _configs = LinkedGamesConfigs.Configs;
        }

        public string GetResult(string custId, string transId3rd, bool isDesktop, string language = "en-US")
        {
            CheckArgument(custId, "custId");
            CheckArgument(transId3rd, "transId3rd");

            var requestData = GenerateRequestData(custId, transId3rd, isDesktop, language);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new LinkedGamesException("Request data:" + _serializer.Serialize(requestData));
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

        private Dictionary<string, string> GenerateRequestData(string custId, string transId3rd, bool isDesktop, string language)
        {
            var languageCode = string.IsNullOrEmpty(language) || !SupportedLanguages.ContainsKey(language)
                                ? SupportedLanguages[DefaultLanguage]
                                : SupportedLanguages[language];
            return new Dictionary<string, string>
            {
                { "SiteId", _configs.SiteId },
                { "ObCustId" , custId },
                { "VendorId" , _configs.VendorId },
                { "Language" , languageCode },
                { "TranId3rd" , transId3rd },
                { "Platform" , isDesktop ? "D" : "M" },
                { "DarkMode" , _configs.DarkMode},
            };
        }

        private bool ValidateResponse(Response responseData, Dictionary<string, string> requestData)
        {
            if (responseData.VendorId != requestData["VendorId"] ||
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
                        throw new ResultCoreException($"Url of LinkedGames send request incorrectly!");
                    }
                }
                else
                {
                    throw new ResultCoreException($"Url of LinkedGames send request incorrectly! Detail request:" +
                        $" {_serializer.Serialize(requestData)}; \n Detail response: {_serializer.Serialize(responseData)}");
                }
            }

            return true;
        }
    }
}