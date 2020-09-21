namespace Fanex.Result.Core.Joker
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.Joker.Models;
    using Fanex.Result.Core.Joker.Ultilities.Serializer;
    using Fanex.Result.Core.Joker.Ultilities.WebClient;

    public interface IJokerResult
    {
        string GetJokerResult(string custId, string gamePlayId, string gameCode, string language = "en-US", string siteId = null);
    }

    public class JokerResult : IJokerResult
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

        private readonly JokerConfigs _configs = JokerConfigs.Configs;
        private readonly IWebClient _client;
        private readonly ISerializer _serializer;

        public JokerResult(IWebClient client = null, ISerializer serializer = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
        }

        public string GetJokerResult(string custId, string gamePlayId, string gameCode, string language = "en-US", string siteId = null)
        {
            CheckArgument(custId, "custId");
            CheckArgument(gamePlayId, "gamePlayId");
            CheckArgument(gameCode, "gameCode");

            var requestData = GenerateRequestData(custId, gamePlayId, gameCode, language, siteId);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new JokerException("Request data:" + _serializer.Serialize(requestData));
            }
            else
            {
                var responseData = _client.UploadString<Request, Response>(_configs.Url, requestData);

                if (ValidateResponse(responseData, requestData))
                {
                    return !string.IsNullOrWhiteSpace(responseData.Result.Url) ?
                        responseData.Result.Url.Replace("http:", string.Empty) :
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

        private Request GenerateRequestData(string custId, string gamePlayId, string gameCode, string language, string siteId)
        {
            return new Request
            {
                TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                Auth = new Auth
                {
                    Login = _configs.Login,
                    Password = _configs.Password,
                    SiteId = siteId ?? _configs.SiteId
                },
                Call = CreateCallObject(custId, gamePlayId, gameCode, language)
            };
        }

        private Call CreateCallObject(string custId, string gamePlayId, string gameCode, string language)
        {
            var languageCode = string.IsNullOrEmpty(language) || !SupportedLanguages.ContainsKey(language)
                                ? SupportedLanguages[DefaultLanguage]
                                : SupportedLanguages[language];

            return new Call
            {
                Seq = Guid.NewGuid().ToString(),
                OBCustId = custId,
                GamePlayId = gamePlayId,
                GameCode = gameCode,
                VendorId = _configs.VendorId,
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
                        throw new ResultCoreException($"Url of Joker Result send request incorrectly!");
                    }
                }
                else
                {
                    throw new ResultCoreException($"Url of Joker Result send request incorrectly! Detail request:" +
                        $" {_serializer.Serialize(requestData)}; Detail response: {_serializer.Serialize(responseData)}");
                }
            }

            return true;
        }
    }
}