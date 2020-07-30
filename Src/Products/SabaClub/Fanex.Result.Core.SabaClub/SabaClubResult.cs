namespace Fanex.Result.Core.SabaClub
{
    using System;
    using System.Collections.Generic;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.SabaClub.Models;
    using Fanex.Result.Core.SabaClub.Ultilities.Serializer;
    using Fanex.Result.Core.SabaClub.Ultilities.WebClient;

    public class SabaClubResult : ISabaClubResult
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

        private readonly SabaClubConfigs _configs = SabaClubConfigs.Configs;
        private readonly IWebClient _client;
        private readonly ISerializer _serializer;

        public SabaClubResult(IWebClient client = null, ISerializer serializer = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
        }

        public string GetResult(string custId, string gameRoundId, string gameId, string siteId, string language = "en-US")
        {
            CheckArgument(custId, "custId");
            CheckArgument(gameRoundId, "gameRoundId");
            CheckArgument(gameId, "gameId");
            CheckArgument(siteId, "siteId");

            var requestData = GenerateRequestData(custId, gameRoundId, gameId, language, siteId);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new SabaClubException("Request data:" + _serializer.Serialize(requestData));
            }
            else
            {
                var responseData = _client.UploadString<Request, Response>(_configs.Url, requestData);

                if (ValidateResponse(responseData, requestData))
                {
                    return !string.IsNullOrWhiteSpace(responseData.Result.RedirectUri) ?
                        responseData.Result.RedirectUri.Replace("http:", string.Empty) :
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

        private Request GenerateRequestData(string custId, string gameRoundId, string gameId, string language, string siteId)
        {
            return new Request
            {
                TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                Auth = new Auth
                {
                    Login = _configs.Login,
                    Password = _configs.Password,
                },
                Call = CreateCallObject(custId, gameRoundId, gameId, language, siteId)
            };
        }

        private Call CreateCallObject(string custId, string gameRoundId, string gameId, string language, string siteId)
        {
            var languageCode = string.IsNullOrEmpty(language) || !SupportedLanguages.ContainsKey(language)
                                ? SupportedLanguages[DefaultLanguage]
                                : SupportedLanguages[language];

            return new Call
            {
                Seq = Guid.NewGuid().ToString(),
                SiteId = siteId,
                OBCustId = custId,
                GameRoundId = gameRoundId,
                GameId = gameId,
                Language = languageCode
            };
        }

        private bool ValidateResponse(Response responseData, Request requestData)
        {
            if (responseData.Result.Seq != requestData.Call.Seq ||
                responseData.Result.ErrorCode != "0")
            {
                if ((DebugMode)_configs.DebugMode == DebugMode.None)
                {
                    if (responseData.Result.ErrorCode != "0")
                    {
                        throw new ResultCoreException(responseData.Result.ErrorDescription);
                    }
                    else
                    {
                        throw new ResultCoreException($"Url of Saba.Club send request incorrectly!");
                    }
                }
                else
                {
                    throw new ResultCoreException($"Url of Saba.Club send request incorrectly! Detail request:" +
                        $" {_serializer.Serialize(requestData)}; Detail response: {_serializer.Serialize(responseData)}");
                }
            }

            return true;
        }
    }
}