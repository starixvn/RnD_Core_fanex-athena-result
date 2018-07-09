namespace Fanex.Result.Core.PokerGames
{
    using System;
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.PokerGames.Models;
    using Fanex.Result.Core.PokerGames.Ultilities.Serializer;
    using Fanex.Result.Core.PokerGames.Ultilities.WebClient;

    public class PokerGamesResult : IPokerGamesResult
    {
        private const string DefaultLanguage = "en-US";

        private static readonly Dictionary<string, string> SupportedLanguages = new Dictionary<string, string>
        {
            { "en-US", "en" },
            { "vi-VN", "vn" }
        };

        private readonly IPokerGamesResultConfigs _configs;
        private readonly IWebClient _client;
        private readonly ISerializer _serializer;

        public PokerGamesResult(
            IWebClient client = null, 
            ISerializer serializer = null,
            IPokerGamesResultConfigs configs = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
            _configs = configs ?? PokerGamesResultConfigs.Configs;
        }

        public string GetResult(string custId, string gameTypeId, string transId3rd, ViewerLevel viewerLevel, string language = "en-US", int siteId = 0)
        {
            CheckArgument(custId, "custId");
            CheckArgument(gameTypeId, "gameTypeId");

            var request = CreateRequest(custId, gameTypeId, transId3rd, siteId, viewerLevel);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                return _serializer.Serialize(request);
            }

            return GetResultUrl(language, request);
        }

        private string GetResultUrl(string language, Request request)
        {
            var response = ExecuteRequest(request);

            var languageCode = string.IsNullOrEmpty(language) || !SupportedLanguages.ContainsKey(language)
                                ? SupportedLanguages[DefaultLanguage]
                                : SupportedLanguages[language];

            return IsReponseValid(response, request)
                ? $"{response.Result.Url}&language={languageCode}"
                : string.Empty;
        }

        private Response ExecuteRequest(Request request)
        {
            try
            {
                return _client.UploadString<Request, Response>(_configs.Url, request);
            }
            catch(Exception ex)
            {
                var exceptionMessage = string.Join(
                    "\r\n",
                    $"Api Url: {_configs.Url}",
                    _serializer.Serialize(request),
                    ex);

                throw new ResultCoreException(exceptionMessage, ex);
            }
        }

        private static void CheckArgument(string arg, string argName)
        {
            if (string.IsNullOrEmpty(arg))
            {
                throw new ResultCoreException($"{argName} can not be null or emtpy");
            }
        }

        private static string MapLevel(ViewerLevel viewLevel)
        {
            switch(viewLevel)
            {
                case ViewerLevel.Company:
                    return "C";

                case ViewerLevel.Super:
                    return "S";

                case ViewerLevel.Master:
                    return "M";

                case ViewerLevel.Agent:
                    return "A";

                default:
                    return "P";
            }
        }

        private Request CreateRequest(string custId, string gameTypeId, string transId3rd, int siteId, ViewerLevel viewerLevel)
        {
            return new Request
            {
                TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                Auth = new Auth
                {
                    Login = _configs.Login,
                    Password = _configs.Password,
                    SiteId = siteId == 0 ? _configs.SiteId : siteId
                },
                Call = CreateCallObject(custId, gameTypeId, transId3rd, viewerLevel)
            };
        }

        private Call CreateCallObject(string custId, string gameTypeId, string transId3rd, ViewerLevel viewerLevel)
        {
            var random = new Random();
            var timestampWithRandom = $"{DateTime.Now.Ticks}-{random.Next(0, int.MaxValue)}";
            var hashedPreInfo = $"{_configs.SiteId}-{custId}-{gameTypeId}-{random.Next(0, int.MaxValue)}";
            var seq = $"{timestampWithRandom}-{hashedPreInfo}";

            return new Call
            {
                Seq = seq,
                OBCustId = custId,
                GameTypeId = gameTypeId,
                TransId3rd = transId3rd,
                Type = MapLevel(viewerLevel)
            };
        }

        private bool IsReponseValid(Response response, Request request)
        {
            DateTime.TryParse(request.TimeStamp, out DateTime requestDateTime);
            DateTime.TryParse(response.TimeStamp, out DateTime responseDateTime);

            var isTimeStampValid = requestDateTime.Equals(responseDateTime);

            if (!isTimeStampValid ||
                response.Result.Seq != request.Call.Seq ||
                response.Result.ErrorCode != 0)
            {
                ThrowException(response, request);
            }

            return true;
        }

        private void ThrowException(Response response, Request request)
        {
            if ((DebugMode)_configs.DebugMode == DebugMode.None)
            {
                if (response.Result.ErrorCode != 0)
                {
                    throw new ResultCoreException(response.Result.ErrorDescription);
                }

                throw new ResultCoreException("Poker Games Result send request incorrectly!");
            }

            throw new ResultCoreException($"Poker Games Result send request incorrectly! Detail request:" +
                    $" {_serializer.Serialize(request)}; Detail response: {_serializer.Serialize(response)}");
        }
    }
}