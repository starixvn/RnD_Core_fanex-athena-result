namespace Fanex.Result.Core.PGSoft
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.PGSoft.Models;
    using Fanex.Result.Core.PGSoft.Ultilities.Serializer;
    using Fanex.Result.Core.PGSoft.Ultilities.WebClient;

    public class PGSoftResult : IPGSoftResult
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

        private readonly PGSoftConfigs _configs;
        private readonly string SectionName;
        private readonly IWebClient _client;
        private readonly ISerializer _serializer;

        public PGSoftResult(IWebClient client = null, ISerializer serializer = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
            SectionName = "PGSoftResultConfigs";
            _configs = ConfigurationManager.GetSection(SectionName) as PGSoftConfigs;
        }

        public string GetResult(PGSoftParam param)
        {
            CheckArgument(param.CustId, "custId");
            CheckArgument(param.GamePlayId, "gamePlayId");

            var requestData = GenerateRequestData(param);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new PGSoftException("Request data:" + _serializer.Serialize(requestData));
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

        private Request GenerateRequestData(PGSoftParam param)
        {
            return new Request
            {
                TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                Auth = new Auth
                {
                    Login = _configs.Login,
                    Password = _configs.Password,
                    SiteId = param.SiteId ?? _configs.SiteId
                },
                Call = CreateCallObject(param)
            };
        }

        private Call CreateCallObject(PGSoftParam param)
        {
            var languageCode = string.IsNullOrEmpty(param.Language) || !SupportedLanguages.ContainsKey(param.Language)
                                ? SupportedLanguages[DefaultLanguage]
                                : SupportedLanguages[param.Language];

            var random = new Random();
            var timestampWithRandom = $"{DateTime.Now.Ticks}-{random.Next(0, int.MaxValue)}";
            var hashedPreInfo = $"{_configs.SiteId}-{param.CustId}-{param.GamePlayId}-{random.Next(0, int.MaxValue)}";
            var seq = $"{timestampWithRandom}-{hashedPreInfo}";

            return new Call
            {
                Seq = seq,
                OBCustId = param.CustId,
                GamePlayId = param.GamePlayId,
                GameId3rd = param.GameId3rd,
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
                        throw new ResultCoreException($"Url of {SectionName} send request incorrectly!");
                    }
                }
                else
                {
                    throw new ResultCoreException($"Url of {SectionName} send request incorrectly! Detail request:" +
                        $" {_serializer.Serialize(requestData)}; Detail response: {_serializer.Serialize(responseData)}");
                }
            }

            return true;
        }
    }
}