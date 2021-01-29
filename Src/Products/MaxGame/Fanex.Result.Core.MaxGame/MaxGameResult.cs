namespace Fanex.Result.Core.MaxGame
{
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.MaxGame.Models;
    using Fanex.Result.Core.MaxGame.Ultilities.Serializer;
    using Fanex.Result.Core.MaxGame.Ultilities.WebClient;

    public interface IMaxGameResult
    {
        string GetResult(string custId, string transId3rd, int betType, string endTime, string siteId = null, string language = "en-US");
    }

    public class MaxGameResult : IMaxGameResult
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

        private readonly MaxGameConfigs _configs;
        private readonly IWebClient _client;
        private readonly ISerializer _serializer;

        public MaxGameResult() : this(null, null)
        {
        }

        protected MaxGameResult(IWebClient client = null, ISerializer serializer = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
            _configs = MaxGameConfigs.Configs;
        }

        public string GetResult(string custId, string transId3rd, int betType, string endTime, string siteId = null, string language = "en-US")
        {
            CheckArgument(custId, "custId");
            CheckArgument(transId3rd, "transId3rd");
            CheckArgument(betType.ToString(), "betType");
            CheckArgument(endTime, "endTime");

            var requestData = GenerateRequestData(custId, transId3rd, betType, endTime, siteId, language);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new MaxGameException("Request data:" + _serializer.Serialize(requestData));
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

        private static Dictionary<string, string> GenerateRequestData(string custId, string transId3rd, int betType, string endTime, string siteId, string language)
        {
            var languageCode = string.IsNullOrEmpty(language) || !SupportedLanguages.ContainsKey(language)
                                ? SupportedLanguages[DefaultLanguage]
                                : SupportedLanguages[language];
            return new Dictionary<string, string>
            {
                { "ObCustId" , custId },
                { "TranId3rd" , transId3rd },
                { "BetType" , betType.ToString() },
                { "SiteId" , siteId },
                { "EndTime" , endTime},
                { "Language" , languageCode }
            };
        }

        private bool ValidateResponse(Response responseData, Dictionary<string, string> requestData)
        {
            if (responseData.ErrorCode != "0")
            {
                if ((DebugMode)_configs.DebugMode == DebugMode.None)
                {
                    if (responseData.ErrorCode != "0")
                    {
                        throw new ResultCoreException(responseData.ErrorMsg);
                    }
                    else
                    {
                        throw new ResultCoreException($"Url of MaxGame send request incorrectly!");
                    }
                }
                else
                {
                    throw new ResultCoreException($"Url of MaxGame send request incorrectly! Detail request:" +
                        $" {_serializer.Serialize(requestData)}; \n Detail response: {_serializer.Serialize(responseData)}");
                }
            }

            return true;
        }
    }
}