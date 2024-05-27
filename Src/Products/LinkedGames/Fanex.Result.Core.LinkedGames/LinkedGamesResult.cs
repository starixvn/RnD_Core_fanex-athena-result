namespace Fanex.Result.Core.LinkedGames
{
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Utilities;

    public interface ILinkedGamesResult
    {
        string GetSoccer5Result(string matchId, string balls, bool isDesktop, bool isDarkMode = false, string language = "en-US", int bettypeId = 3604);
    }

    public class LinkedGamesResult : ILinkedGamesResult
    {
        private readonly LinkedGamesConfigs _configs;
        private readonly ISerializer _serializer;

        public LinkedGamesResult(LinkedGamesConfigs configs, ISerializer serializer = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            _configs = configs;
        }

        public string GetSoccer5Result(string matchId, string balls, bool isDesktop, bool isDarkMode = false, string language = "en-US", int bettypeId = 3604)
        {
            Helper.CheckArgument(matchId, "matchId");
            Helper.CheckArgument(balls, "balls");

            var requestData = GenerateRequestData();
            if ((DebugMode)_configs.Debug == DebugMode.DontSendRequest)
            {
                throw new LinkedGamesException("Request data:" + _serializer.Serialize(requestData));
            }
            else
            {
                return $"{_configs.Url}?{Helper.DictionaryToString(requestData)}";
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
                    { "DarkMode" , isDarkMode ? "1" : "0"},
                };
            }
        }
    }
}