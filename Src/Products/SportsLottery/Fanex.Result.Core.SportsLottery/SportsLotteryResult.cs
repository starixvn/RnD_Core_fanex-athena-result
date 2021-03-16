namespace Fanex.Result.Core.SportsLottery
{
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.SportsLottery.Models;
    using Fanex.Result.Core.SportsLottery.Ultilities.Serializer;
    using Fanex.Result.Core.SportsLottery.Ultilities.WebClient;
    using System;
    using System.Collections.Generic;

    public interface ISportsLotteryResult
    {
        string GetResultPage(bool isDesktop, string language = SportsLotteryConstants.DefaultLanguage);

        string GetResultDetail(string custId, string transId3rd, bool isDesktop, string language = SportsLotteryConstants.DefaultLanguage);
    }

    public class SportsLotteryResult : ISportsLotteryResult
    {
        private readonly SportsLotteryConfigs _configs;
        private readonly IWebClient _client;
        private readonly ISerializer _serializer;

        public SportsLotteryResult() : this(null, null)
        {
        }

        protected SportsLotteryResult(IWebClient client = null, ISerializer serializer = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
            _configs = SportsLotteryConfigs.Configs;
        }

        public string GetResultPage(bool isDesktop, string language = SportsLotteryConstants.DefaultLanguage)
        {
            var languageCode = SportsLotteryConstants.GetLanguageCode(language);
            var requestData = GenerateRequestPage(languageCode);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new SportsLotteryException("Request Result Page data:" + _serializer.Serialize(requestData));
            }
            else
            {
                var responseData = _client.UploadString<RequestResultPage, ResponseResultPage>(_configs.UrlPage, requestData, _configs.ProxyIP, _configs.ProxyPort);

                if (ValidateResponsePage(responseData, requestData))
                {
                    var url = AddMoreInfoToResult(responseData.Result.Url, languageCode, isDesktop);
                    return RemoveWebProtocol(url);
                }
            }

            return string.Empty;
        }

        public string GetResultDetail(string custId, string transId3rd, bool isDesktop, string language = SportsLotteryConstants.DefaultLanguage)
        {
            CheckArgument(custId, "custId");
            CheckArgument(transId3rd, "transId3rd");

            var languageCode = SportsLotteryConstants.GetLanguageCode(language);
            var requestData = GenerateRequestDetail(custId, transId3rd, isDesktop, languageCode);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new SportsLotteryException("Request Result Detail data:" + _serializer.Serialize(requestData));
            }
            else
            {
                var responseData = _client.QueryString<ResponseResultDetail>(_configs.UrlDetail, requestData, _configs.ProxyIP, _configs.ProxyPort);

                if (ValidateResponseDetail(responseData, requestData))
                {
                    return RemoveWebProtocol(responseData.Url);
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

        private RequestResultPage GenerateRequestPage(string languageCode)
        {
            var random = new Random();
            var timestampWithRandom = $"{DateTime.Now.Ticks}-{random.Next(0, int.MaxValue)}";
            var hashedPreInfo = $"{_configs.SiteId}-{random.Next(0, int.MaxValue)}";
            var seq = $"{timestampWithRandom}-{hashedPreInfo}";

            return new RequestResultPage
            {
                TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                Seq = seq,
                SiteId = _configs.SiteId,
                Language = languageCode
            };
        }

        private Dictionary<string, string> GenerateRequestDetail(string custId, string transId3rd, bool isDesktop, string languageCode)
            => new Dictionary<string, string>
            {
                { "SiteId", _configs.SiteId },
                { "ObCustId" , custId },
                { "Language" , languageCode },
                { "TranId3rd" , transId3rd },
                { "Platform" , isDesktop ? SportsLotteryConstants.Device.Desktop : SportsLotteryConstants.Device.Mobile},
                { "DarkMode" , _configs.DarkMode},
            };

        private bool ValidateResponsePage<TIn>(ResponseResultPage responseData, TIn requestData)
        {
            if (responseData.Result.ErrorCode != "0")
            {
                if ((DebugMode)_configs.DebugMode == DebugMode.None)
                {
                    throw new SportsLotteryException(responseData.Result.ErrorDescription);
                }
                else
                {
                    throw new SportsLotteryException($"Url of Sports Lottery send request incorrectly! Result Page request:" +
                        $" {_serializer.Serialize(requestData)}; \n Result Page response: {_serializer.Serialize(responseData)}");
                }
            }

            return true;
        }

        private bool ValidateResponseDetail<TIn>(ResponseResultDetail responseData, TIn requestData)
        {
            if (responseData.ErrorCode != "0")
            {
                if ((DebugMode)_configs.DebugMode == DebugMode.None)
                {
                    throw new SportsLotteryException(responseData.ErrorMsg);
                }
                else
                {
                    throw new SportsLotteryException($"Url of Sports Lottery send request incorrectly! Result Detail request:" +
                        $" {_serializer.Serialize(requestData)}; \n Result Detail response: {_serializer.Serialize(responseData)}");
                }
            }

            return true;
        }

        private static string RemoveWebProtocol(string responseUrl)
            => !string.IsNullOrWhiteSpace(responseUrl) ?
                        responseUrl.Replace("http:", string.Empty) :
                        string.Empty;

        private string AddMoreInfoToResult(string resultUrl, string languageCode, bool isDesktop)
        {
            var platform = isDesktop ? SportsLotteryConstants.Device.Desktop : SportsLotteryConstants.Device.Mobile;

            return $"{resultUrl}&l={languageCode}&pl={platform}&s={_configs.SiteId}";
        }
    }
}