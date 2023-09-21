namespace Fanex.Result.Core.KenoLotteryTG
{
    using System;
    using System.Collections.Generic;
    using Fanex.Result.Core.ConfigurationHelper;
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
        private readonly string SectionName;

        public KenoLotteryTGResult(string sectionName, IConfigurationHelper configHelper) : this(sectionName, configHelper, null, null)
        {
        }

        protected KenoLotteryTGResult(string sectionName, IConfigurationHelper configHelper, IWebClient client = null, ISerializer serializer = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
            SectionName = sectionName;
            _configs = configHelper.GetSection(sectionName) as KenoLotteryTGConfigs;
        }

        public string GetResult(string custId, bool isDesktop, DateTime eventDate, string homeUrl, string language = "en-US")
        {
            CheckArgument(custId, "custId");

            var requestData = GenerateRequestData(custId, isDesktop, eventDate, language, homeUrl);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new ResultCoreException("Request data:" + _serializer.Serialize(requestData));
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
                if ((DebugMode)_configs.DebugMode == DebugMode.None)
                {
                    throw new ResultCoreException(responseData.ErrorMsg);
                }
                else
                {
                    throw new ResultCoreException($"Url of {SectionName} send request incorrectly! Detail request:" +
                        $" {_serializer.Serialize(requestData)}; \n Detail response: {_serializer.Serialize(responseData)}");
                }
            }

            return true;
        }
    }
}