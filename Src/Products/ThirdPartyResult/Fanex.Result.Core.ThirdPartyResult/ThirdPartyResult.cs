namespace Fanex.Result.Core.ThirdParty
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.ThirdParty.Models;

    public interface IThirdPartyResult
    {
        string GetResultPOST(ResultRequest resultRequest);
        string GetResultGET(ResultRequest resultRequest);
    }

    public class ThirdPartyResult : IThirdPartyResult
    {
        private readonly ThirdPartyConfigs _configs;
        private readonly IWebClient _client;
        private readonly ISerializer _serializer;
        private readonly string SectionName;

        public ThirdPartyResult(string sectionName) : this(sectionName, null, null)
        {
            SectionName = sectionName;
        }

        protected ThirdPartyResult(string sectionName, IWebClient client, ISerializer serializer)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
            _configs = ConfigurationManager.GetSection(sectionName) as ThirdPartyConfigs;
        }

        public string GetResultPOST(ResultRequest resultRequest)
        {
            Helper.CheckNotZero(resultRequest.CustId, "custId");
            Helper.CheckArgument(resultRequest.TranId3rd, "transId3rd");
            Helper.CheckArgument(resultRequest.GameId3rd, "gameId3rd");

            var requestData = GenerateRequestData(resultRequest);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new ResultCoreException("Request data:" + _serializer.Serialize(requestData));
            }
            else
            {
                var responseData = _client.UploadString<Request, Response>(_configs.Url, requestData, _configs.ProxyIP, _configs.ProxyPort);

                if (ValidateResponse(responseData, requestData))
                {
                    return !string.IsNullOrWhiteSpace(responseData.Url) ?
                        responseData.Url.Replace("http:", string.Empty) :
                        string.Empty;
                }
            }

            return string.Empty;
        }

        public string GetResultGET(ResultRequest resultRequest)
        {
            Helper.CheckNotZero(resultRequest.CustId, "custId");
            Helper.CheckArgument(resultRequest.TranId3rd, "transId3rd");
            Helper.CheckArgument(resultRequest.GameId3rd, "gameId3rd");

            var requestData = GenerateRequestData();

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new ResultCoreException("Request data:" + _serializer.Serialize(requestData));
            }
            else
            {
                var responseData = _client.QueryString<Response>(_configs.Url, requestData, _configs.ProxyIP, _configs.ProxyPort);

                if (ValidateResponse(responseData))
                {
                    return !string.IsNullOrWhiteSpace(responseData.Url) ?
                        responseData.Url.Replace("http:", string.Empty) :
                        string.Empty;
                }
            }

            return string.Empty;

            bool ValidateResponse(Response responseData)
            {
                if (responseData.ErrorCode != 0)
                {
                    if ((DebugMode)_configs.DebugMode == DebugMode.None)
                    {
                        var message = $"{SectionName} Result API: {responseData.ErrorMsg}";
                        message += $"\n Detail request: {Helper.DictionaryToString(requestData)}";
                        message += $"\n Detail response: {_serializer.Serialize(responseData)}";
                        throw new ResultCoreException(message);
                    }
                    else
                    {
                        throw new ResultCoreException($"Url of {SectionName} send request incorrectly! Detail request:" +
                            $" {Helper.DictionaryToString(requestData)}; \n Detail response: {_serializer.Serialize(responseData)}");
                    }
                }

                return true;
            }

            Dictionary<string, string> GenerateRequestData()
            {
                var languageCode = Language.GetLanguageCode(resultRequest.Language);
                var random = new Random();
                var timestampWithRandom = $"{DateTime.Now.Ticks}-{random.Next(0, int.MaxValue)}";
                var hashedPreInfo = $"{resultRequest.SiteId}-{resultRequest.CustId}-{resultRequest.TranId3rd}-{random.Next(0, int.MaxValue)}";
                var seq = $"{timestampWithRandom}-{hashedPreInfo}";

                return new Dictionary<string, string>
                {
                    { "Timestamp" ,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") },
                    { "SiteId", string.IsNullOrEmpty(resultRequest.SiteId) || resultRequest.SiteId == "0" ? _configs.SiteId : resultRequest.SiteId},
                    { "Seq" , seq },
                    { "ObCustId" , resultRequest.CustId.ToString() },
                    { "Language" , languageCode },
                    { "TranId3rd" , resultRequest.TranId3rd },
                    { "GameId3rd" , resultRequest.GameId3rd },
                    { "VendorId" , _configs.VendorId.ToString() },
                    { "CurrencyId" , resultRequest.CurrencyId.ToString() },
                    { "BetType" , resultRequest.BetTypeId.ToString() },
                    { "EndTime" , resultRequest.EndTime },
                    { "OwTransId" , resultRequest.OwTransId },
                    { "Platform" , resultRequest.Platform},
                    { "DarkMode" , resultRequest.IsDarkMode ? "1" : "0" },
                    { "GMT" , resultRequest.GMT},
                };
            }
        }

        private Request GenerateRequestData(ResultRequest result)
        {
            var languageCode = Language.GetLanguageCode(result.Language);
            var random = new Random();
            var timestampWithRandom = $"{DateTime.Now.Ticks}-{random.Next(0, int.MaxValue)}";
            var hashedPreInfo = $"{result.SiteId}-{result.CustId}-{result.TranId3rd}-{random.Next(0, int.MaxValue)}";
            var seq = $"{timestampWithRandom}-{hashedPreInfo}";

            return new Request
            {
                Seq = seq,
                VendorId = _configs.VendorId,
                SiteId = string.IsNullOrEmpty(result.SiteId) || result.SiteId == "0" ? _configs.SiteId : result.SiteId,
                ObCustId = result.CustId,
                TranId3rd = result.TranId3rd,
                Language = languageCode,
                GameId3rd = result.GameId3rd,
                CurrencyId = result.CurrencyId,
                TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                BetType = result.BetTypeId.ToString(),
                EndTime = result.EndTime,
                OwTransId = result.OwTransId,
                LangCode = languageCode,
                Platform = result.Platform,
                DarkMode = result.IsDarkMode ? "1" : "0",
                GMT = result.GMT
            };
        }

        private bool ValidateResponse(Response responseData, Request requestData)
        {
            if (responseData.ErrorCode != 0)
            {
                if ((DebugMode)_configs.DebugMode == DebugMode.None)
                {
                    var message = $"{SectionName} Result API: {responseData.ErrorMsg}";
                    message += $"\n Detail request: {_serializer.Serialize(requestData)}";
                    message += $"\n Detail response: {_serializer.Serialize(responseData)}";
                    throw new ResultCoreException(message);
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