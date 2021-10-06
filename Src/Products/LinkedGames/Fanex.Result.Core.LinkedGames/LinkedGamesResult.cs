namespace Fanex.Result.Core.LinkedGames
{
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.LinkedGames.Models;

    public interface ILinkedGamesResult
    {
        string GetResult(string custId, string transId3rd, bool isDesktop, string language = "en-US");

        string GetSoccer5Result(string matchId, string balls, bool isDesktop, string language = "en-US", int bettypeId = 3604);
    }

    public class LinkedGamesResult : ILinkedGamesResult
    {
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

        public string GetSoccer5Result(string matchId, string balls, bool isDesktop, string language = "en-US", int bettypeId = 3604)
        {
            Helper.CheckArgument(matchId, "matchId");
            Helper.CheckArgument(balls, "balls");

            var requestData = GenerateRequestData();
            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new LinkedGamesException("Request data:" + _serializer.Serialize(requestData));
            }
            else
            {
                return $"{_configs.UrlSoccer5}?{Helper.DictionaryToString(requestData)}";
            }

            Dictionary<string, string> GenerateRequestData()
            {
                var languageCode = Language.GetLanguageCode(language);
                return new Dictionary<string, string>
                {
                    { "MatchId", matchId},
                    { "SiteId", _configs.SiteId },
                    { "LangCode" , languageCode },
                    { "Balllist" , balls },
                    { "BettypeId" , bettypeId.ToString() },
                    { "Platform" , isDesktop ? "D" : "M" },
                    { "DarkMode" , _configs.DarkMode},
                };
            }
        }

        public string GetResult(string custId, string transId3rd, bool isDesktop, string language = "en-US")
        {
            Helper.CheckArgument(custId, "custId");
            Helper.CheckArgument(transId3rd, "transId3rd");

            var requestData = GenerateRequestData();

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new LinkedGamesException("Request data:" + _serializer.Serialize(requestData));
            }
            else
            {
                var responseData = _client.QueryString<Response>(_configs.Url, requestData, _configs.ProxyIPServer, _configs.ProxyPortServer);

                if (ValidateResponse(responseData))
                {
                    return RemoveWebProtocol(responseData.Url);
                }
            }

            return string.Empty;

            Dictionary<string, string> GenerateRequestData()
            {
                var languageCode = Language.GetLanguageCode(language);
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

            bool ValidateResponse(Response responseData)
            {
                if (responseData.ErrorCode != 0)
                {
                    if ((DebugMode)_configs.DebugMode == DebugMode.None)
                    {
                        throw new ResultCoreException(responseData.ErrorMsg);
                    }
                    else
                    {
                        throw new ResultCoreException($"Url of LinkedGames send request incorrectly! Detail request:" +
                            $" {Helper.DictionaryToString(requestData)}; \n Detail response: {_serializer.Serialize(responseData)}");
                    }
                }

                return true;
            }
        }

        private static string RemoveWebProtocol(string url)
            => !string.IsNullOrWhiteSpace(url) ? url.Replace("http:", string.Empty) : string.Empty;
    }
}