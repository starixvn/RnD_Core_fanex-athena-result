namespace Fanex.Result.Core.Habanero
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.Habanero.Models;

    public interface IHabaneroResult
    {
        string GetResult(string custId, string transId3rd, string gameId3rd, string siteId, string language = "en-US");
    }

    public class HabaneroResult : IHabaneroResult
    {
        private readonly HabaneroConfigs _configs;
        private readonly IWebClient _client;
        private readonly ISerializer _serializer;
        private readonly string SectionName;

        public HabaneroResult(string sectionName) : this(sectionName, null, null)
        {
            SectionName = sectionName;
        }

        protected HabaneroResult(string sectionName, IWebClient client, ISerializer serializer)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
            _configs = ConfigurationManager.GetSection(sectionName) as HabaneroConfigs;
        }

        public string GetResult(string custId, string transId3rd, string gameId3rd, string siteId, string language = "en-US")
        {
            Helper.CheckArgument(custId, "custId");
            Helper.CheckArgument(transId3rd, "transId3rd");
            Helper.CheckArgument(gameId3rd, "gameId3rd");
            Helper.CheckArgument(siteId, "siteId");

            var requestData = GenerateRequestData(custId, transId3rd, gameId3rd, language, siteId);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new ResultCoreException("Request data:" + _serializer.Serialize(requestData));
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

        private static Dictionary<string, string> GenerateRequestData(string custId, string transId3rd, string gameId3rd, string language, string siteId)
        {
            var languageCode = Language.GetLanguageCode(language);
            var random = new Random();
            var timestampWithRandom = $"{DateTime.Now.Ticks}-{random.Next(0, int.MaxValue)}";
            var hashedPreInfo = $"{siteId}-{custId}-{transId3rd}-{random.Next(0, int.MaxValue)}";
            var seq = $"{timestampWithRandom}-{hashedPreInfo}";

            return new Dictionary<string, string>
            {
                { "Timestamp" ,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") },
                { "SiteId", siteId},
                { "Seq" , seq },
                { "ObCustId" , custId },
                { "Language" , languageCode },
                { "TranId3rd" , transId3rd },
                { "GameId3rd" , gameId3rd },
            };
        }

        private bool ValidateResponse(Response responseData, Dictionary<string, string> requestData)
        {
            if (responseData.Seq != requestData["Seq"] || responseData.ErrorCode != 0)
            {
                if ((DebugMode)_configs.DebugMode == DebugMode.None)
                {
                    throw new ResultCoreException($"{SectionName} Result API: {responseData.ErrorMsg}");
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