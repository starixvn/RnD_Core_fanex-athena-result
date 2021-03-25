namespace Fanex.Result.Core.PPLiveCasino
{
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.PPLiveCasino.Models;
    using Fanex.Result.Core.PPLiveCasino.Ultilities.Serializer;
    using Fanex.Result.Core.PPLiveCasino.Ultilities.WebClient;
    using System.Collections.Generic;

    public interface IPPLiveCasinoResult
    {
        string GetResult(string custId, string gameId3rd, int betType, string language = "en-US");
    }

    public class PPLiveCasinoResult : IPPLiveCasinoResult
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

        private readonly PPLiveCasinoConfigs _configs;
        private readonly IWebClient _client;
        private readonly ISerializer _serializer;

        public PPLiveCasinoResult() : this(null, null)
        {
        }

        protected PPLiveCasinoResult(IWebClient client = null, ISerializer serializer = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
            _configs = PPLiveCasinoConfigs.Configs;
        }

        public string GetResult(string custId, string gameId3rd, int betType, string language = "en-US")
        {
            CheckArgument(custId, "custId");
            CheckArgument(gameId3rd, "gameId3rd");

            var requestData = GenerateRequestData(custId, gameId3rd, betType, language);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new PPLiveCasinoException("Request data:" + _serializer.Serialize(requestData));
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
                throw new PPLiveCasinoException($"{argName} can not be null or emtpy");
            }
        }

        private Dictionary<string, string> GenerateRequestData(string custId, string gameId3rd, int betType, string language)
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
                { "GameId3rd" , gameId3rd },
                { "BetType" , betType.ToString()}
            };
        }

        private bool ValidateResponse(Response responseData, Dictionary<string, string> requestData)
        {
            if (responseData.ErrorCode != "0")
            {
                if ((DebugMode)_configs.DebugMode == DebugMode.None)
                {
                    throw new PPLiveCasinoException(responseData.ErrorMsg);
                }
                else
                {
                    throw new PPLiveCasinoException($"Url of PPLiveCasino send request incorrectly! Detail request:" +
                        $" {_serializer.Serialize(requestData)}; \n Detail response: {_serializer.Serialize(responseData)}");
                }
            }

            return true;
        }
    }
}