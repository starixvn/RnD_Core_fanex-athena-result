namespace Fanex.Result.Core.LinkedGames
{
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Utilities;

    public interface ILinkedGamesResult
    {
        string GetSoccer5Result(string matchId, string balls, bool isDesktop, string language = "en-US", int bettypeId = 3604);
    }

    public class LinkedGamesResult : ILinkedGamesResult
    {
        private readonly LinkedGamesConfigs _configs;
        private readonly ISerializer _serializer;

        public LinkedGamesResult() : this(null)
        {
        }

        protected LinkedGamesResult(ISerializer serializer = null)
        {
            _serializer = serializer ?? new JsonSerializer();
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
    }
}