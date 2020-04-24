namespace Fanex.Result.Core.VirtualGames
{
    using System;
    using Fanex.Result.Core.Base;
    using Fanex.Result.Core.Base.Exception;
    using Fanex.Result.Core.VirtualGames.Models;
    using Fanex.Result.Core.VirtualGames.Ultilities;
    using Fanex.Result.Core.VirtualGames.Ultilities.Serializer;
    using Fanex.Result.Core.VirtualGames.Ultilities.WebClient;

    public class VirtualGamesResult : IVirtualGamesResult
    {
        private readonly int GMT8 = 8;
        private readonly VirtualGamesConfigs _configs;
        private readonly IWebClient _client;
        private readonly ISerializer _serializer;
        private readonly INewGui _newGui;

        public VirtualGamesResult() : this(null, null, null)
        {

        }

        public VirtualGamesResult(IWebClient client = null, ISerializer serializer = null, INewGui newGui = null)
        {
            _serializer = serializer ?? new JsonSerializer();
            _client = client ?? new JsonWebClient(_serializer);
            _newGui = newGui ?? new NewGui();
            _configs = VirtualGamesConfigs.Configs;
        }

        public string GetResult(string platform, string language = "en-US")
        {
            var requestData = GenerateRequestData();

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new VirtualGamesException("Request data:" + _serializer.Serialize(requestData));
            }

            var responseData = _client.UploadString<Request, Response>(_configs.UrlResult, requestData);
            var isValidResponse = ValidateResponse(responseData, requestData);
            if (!isValidResponse)
            {
                throw new ResultCoreException($"Url of Virtual Games send request incorrectly! Detail request:" +
                    $" {_serializer.Serialize(requestData)}; Detail response: {_serializer.Serialize(responseData)}");
            }

            return AddMoreInfoToResult(responseData.Result.Url, language, platform);
        }

        public string GetDetailResult(string gameId, string platform, string language = "en-US")
        {
            CheckArgument(gameId, "gameId");

            var requestData = GenerateDetailRequestData(gameId);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new VirtualGamesException("Request data:" + _serializer.Serialize(requestData));
            }

            var responseData = _client.UploadString<RequestDetail, Response>(_configs.UrlDetailResult, requestData);
            var isValidResponse = ValidateResponseDetail(responseData, requestData);
            if (!isValidResponse)
            {
                throw new ResultCoreException($"Url of Virtual Games send request incorrectly! Detail request:" +
                    $" {_serializer.Serialize(requestData)}; Detail response: {_serializer.Serialize(responseData)}");
            }

            return AddMoreInfoToDetailResult(responseData.Result.Url, language, platform);
        }

        private static void CheckArgument(string arg, string argName)
        {
            if (string.IsNullOrEmpty(arg))
            {
                throw new ResultCoreException($"{argName} can not be null or emtpy");
            }
        }

        private Request GenerateRequestData()
        {
            return new Request
            {
                TimeStamp = DateTime.UtcNow.AddHours(GMT8).ToString("yyyy-MM-dd HH:mm:ss.fff"),
                Auth = new Auth
                {
                    Login = _configs.Login,
                    Password = _configs.Password
                },
                Call = new Call
                {
                    Seq = _newGui.CreateNewId(),
                    SiteId = _configs.SiteId
                }
            };
        }

        private RequestDetail GenerateDetailRequestData(string gameId)
        {
            return new RequestDetail
            {
                TimeStamp = DateTime.UtcNow.AddHours(GMT8).ToString("yyyy-MM-dd HH:mm:ss.fff"),
                Auth = new Auth
                {
                    Login = _configs.Login,
                    Password = _configs.Password
                },
                Call = new CallDetail
                {
                    Seq = _newGui.CreateNewId(),
                    SiteId = _configs.SiteId,
                    GameId = gameId
                }
            };
        }

        private static bool ValidateResponse(Response responseData, Request requestData)
            => responseData.Result.Seq == requestData.Call.Seq &&
                responseData.Result.ErrorCode == "0";

        private static bool ValidateResponseDetail(Response responseData, RequestDetail requestData)
            => responseData.Result.Seq == requestData.Call.Seq &&
                responseData.Result.ErrorCode == "0";

        private static string AddMoreInfoToResult(string virtualGamesResult, string language, string platform)
        {
            var languageCode = VirtualGamesLanguageConstants.GetLanguageCode(language);
            var validPlatform = VirtualGamesDeviceConstants.GetSupportedDevice(platform);

            return $"{virtualGamesResult}&l={languageCode}&pl={validPlatform}";
        }

        private string AddMoreInfoToDetailResult(string virtualGamesResult, string language, string platform)
            => $"{AddMoreInfoToResult(virtualGamesResult, language, platform)}&s={_configs.SiteId}";
    }
}