namespace Fanex.Result.Core.KenoLotteryTG
{
    using System;
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.KenoLotteryTG.Models;

    public interface IKenoLotteryTGResult
    {
        string GetResult(string custId, bool isDesktop, DateTime eventDate, string homeUrl, string language = "en-US");
    }

    public class KenoLotteryTGResult : IKenoLotteryTGResult
    {
        private readonly KenoLotteryTGConfigs _configs;
        private readonly IWebClient _client;
        private readonly ISerializer _serializer;

        public KenoLotteryTGResult(KenoLotteryTGConfigs configs) : this(configs, null, null)
        {
        }

        protected KenoLotteryTGResult(KenoLotteryTGConfigs configs, IWebClient client = null, ISerializer serializer = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
            _configs = configs;
        }

        public string GetResult(string custId, bool isDesktop, DateTime eventDate, string homeUrl, string language = "en-US")
        {
            CheckArgument(custId, "custId");

            var requestData = GenerateRequestData(custId, isDesktop, eventDate, language, homeUrl);

            if ((DebugMode)_configs.Debug == DebugMode.DontSendRequest)
            {
                throw new ResultCoreException("Request data:" + _serializer.Serialize(requestData));
            }
            else
            {
                var responseData = _client.QueryString<Response>(_configs.Url, requestData, _configs.ProxyIP, _configs.ProxyPort);

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

        private Dictionary<string, string> GenerateRequestData(string custId, bool isDesktop, DateTime eventDate, string language, string homeUrl)
        {
            var languageCode = Language.GetLanguageCode(language);

            return new Dictionary<string, string>
            {
                { "ObCustId" , custId },
                { "App" , isDesktop ? "W" : "S" },
                { "Language" , languageCode },
                { "EventDate" , eventDate.ToString("yyyyMMdd") },
                { "SiteId" , _configs.SiteId},
                { "HomeURL" , homeUrl}
            };
        }

        private bool ValidateResponse(Response responseData, Dictionary<string, string> requestData)
        {
            if (responseData.ErrorCode != 0)
            {
                if ((DebugMode)_configs.Debug == DebugMode.None)
                {
                    throw new ResultCoreException(responseData.ErrorMsg);
                }
                else
                {
                    throw new ResultCoreException($"Configuraion {_serializer.Serialize(_configs)} send request incorrectly! Detail request:" +
                        $" {_serializer.Serialize(requestData)}; \n Detail response: {_serializer.Serialize(responseData)}");
                }
            }

            return true;
        }
    }
}