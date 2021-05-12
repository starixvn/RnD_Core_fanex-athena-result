namespace Fanex.Result.Core.VGaming
{
    using System;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.Base.Utilities;
    using Fanex.Result.Core.VGaming.Models;

    public interface IVGamingResult
    {
        string GetResult(string custId, string gameRoundId, string gameId, string siteId, int sportType, string currencyName, string language = "en-US");
    }

    public class VGamingResult : IVGamingResult
    {
        private const int FreebetSportTypeId = 173;

        private readonly VGamingConfigs _configs = VGamingConfigs.Configs;
        private readonly IWebClient _client;
        private readonly ISerializer _serializer;

        public VGamingResult(IWebClient client = null, ISerializer serializer = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
        }

        public string GetResult(string custId, string gameRoundId, string gameId, string siteId, int sportType, string currencyName, string language = "en-US")
        {
            CheckArgument(custId, "custId");
            CheckArgument(gameRoundId, "gameRoundId");
            CheckArgument(gameId, "gameId");
            CheckArgument(siteId, "siteId");
            CheckArgument(currencyName, "currencyName");

            var requestData = GenerateRequestData(custId, gameRoundId, gameId, language, siteId, sportType, currencyName);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new ResultCoreException("Request data:" + _serializer.Serialize(requestData));
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

        private Request GenerateRequestData(string custId, string gameRoundId, string gameId, string language, string siteId, int sportType, string currencyName)
        {
            return new Request
            {
                TimeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                Auth = new Auth
                {
                    Login = _configs.Login,
                    Password = _configs.Password,
                },
                Call = CreateCallObject(custId, gameRoundId, gameId, language, siteId, sportType, currencyName)
            };
        }

        private Call CreateCallObject(string custId, string gameRoundId, string gameId, string language, string siteId, int sportType, string currencyName)
        {
            var languageCode = Language.GetLanguageCode(language);

            return new Call
            {
                Seq = Guid.NewGuid().ToString(),
                SiteId = siteId,
                OBCustId = custId,
                GameRoundId = gameRoundId,
                GameId = gameId,
                Language = languageCode,
                Freebet = sportType == FreebetSportTypeId,
                CurrencyName = currencyName
            };
        }

        private bool ValidateResponse(Response responseData, Request requestData)
        {
            if (responseData.Result.Seq != requestData.Call.Seq ||
                responseData.Result.ErrorCode != 0)
            {
                if ((DebugMode)_configs.DebugMode == DebugMode.None)
                {
                    throw new ResultCoreException(responseData.Result.ErrorDescription);
                }
                else
                {
                    throw new ResultCoreException($"Url of VGaming send request incorrectly! Detail request:" +
                        $" {_serializer.Serialize(requestData)}; Detail response: {_serializer.Serialize(responseData)}");
                }
            }

            return true;
        }
    }
}