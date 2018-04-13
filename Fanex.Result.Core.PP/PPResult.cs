namespace Fanex.Result.Core.PP
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.PP.Models;
    using Fanex.Result.Core.PP.Ultilities.Serializer;
    using Fanex.Result.Core.PP.Ultilities.WebClient;

    public class PPResult : IPPResult
    {
        private const string DefaultLanguage = "en-US";

        private static readonly Dictionary<string, string> SupportedLanguages = new Dictionary<string, string>
        {
            { "en-US", "en" },
            { "zh-CN", "cs" },
            { "zh-Hans", "zhcn" },
            { "th-TH", "th" },
            { "vi-VN", "vn" },
            { "id-ID", "id" }
        };

        private readonly PPConfigs _configs = PPConfigs.Configs;
        private readonly IWebClient _client;
        private readonly ISerializer _serializer;

        public PPResult(IWebClient client = null, ISerializer serializer = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
        }

        public string GetPPResult(string custId, string gamePlayId, string language = "en-US", string siteId = null)
        {
            CheckArgument(custId, "custId");
            CheckArgument(gamePlayId, "gamePlayId");

            var requestData = GenerateRequestData(custId, gamePlayId, language, siteId);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                return _serializer.Serialize(requestData);
            }
            else
            {
                var responseData = _client.UploadString<Request, Response>(_configs.Url, requestData);

                if (ValidateResponse(responseData, requestData))
                {
                    return responseData.Result.Url;
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

        private Request GenerateRequestData(string custId, string gamePlayId, string language, string siteId)
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
                Call = CreateCallObject(custId, gamePlayId, language)
            };
        }

        private Call CreateCallObject(string custId, string gamePlayId, string language)
        {
            var languageCode = string.IsNullOrEmpty(language) || !SupportedLanguages.ContainsKey(language)
                                ? SupportedLanguages[DefaultLanguage]
                                : SupportedLanguages[language];

            var random = new Random();
            var timestampWithRandom = $"{DateTime.Now.Ticks}-{random.Next(0, int.MaxValue)}";
            var hashedPreInfo = $"{_configs.SiteId}-{custId}-{gamePlayId}-{random.Next(0, int.MaxValue)}";
            var seq = $"{timestampWithRandom}-{hashedPreInfo}";

            return new Call
            {
                Seq = seq,
                OBCustId = custId,
                GamePlayId = gamePlayId,
                VendorId = _configs.VendorId,
                Language = languageCode
            };
        }

        private bool ValidateResponse(Response responseData, Request requestData)
        {
            DateTime.TryParse(requestData.TimeStamp, out DateTime requestDateTime);
            DateTime.TryParse(responseData.TimeStamp, out DateTime responseDateTime);
            var isEqualTimeStamp = requestDateTime.Equals(responseDateTime);

            if (!isEqualTimeStamp ||
                responseData.Result.Seq != requestData.Call.Seq ||
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
                        throw new ResultCoreException("PP Result send request incorrectly!");
                    }
                }
                else
                {
                    throw new ResultCoreException($"PP Result send request incorrectly! Detail request:" +
                        $" {_serializer.Serialize(requestData)}; Detail response: {_serializer.Serialize(responseData)}");
                }
            }

            return true;
        }
    }
}