namespace Fanex.Result.Core.GlobalGaming
{
    using System;
    using Base;
    using Base.Models;
    using DTO;
    using Exceptions;
    using Models;

    public class GlobalGamingResultApi : Base3rdResultApi
    {
        private GlobalGamingConfigs _configs;

        public GlobalGamingResultApi()
            : base()
        {
            _configs = GlobalGamingConfigs.Configs;
        }

        public override string GetResultUrl<T>(T data)
        {
            var globalGamingData = data as GlobalGamingDTO;
            SiteId = globalGamingData.SiteId ?? _configs.SiteId;
            ValidateArguments(globalGamingData);

            Seq = GenerateSeqCode(globalGamingData.CustId, globalGamingData.GameId, globalGamingData.SiteId);
            var request = CreateRequestObject(globalGamingData);
            var jsonRequest = SerializeJsonObject(request);
            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                throw new GlobalGamingException($"Debug Mode 1 with request data {jsonRequest}");
            }

            var jsonResponse = SendPostRequest(_configs.Url, jsonRequest);
            var response = DeserializeJsonObject<Response>(jsonResponse);
            ValidateResponse(response, request);

            return response.Result.ResultUrl;
        }

        private void ValidateArguments(GlobalGamingDTO data)
        {
            if (string.IsNullOrEmpty(data.CustId))
            {
                throw new GlobalGamingException("custId is null or empty!");
            }

            if (string.IsNullOrEmpty(data.GameId))
            {
                throw new GlobalGamingException("gameId is null or empty!");
            }

            if (string.IsNullOrEmpty(SiteId))
            {
                throw new GlobalGamingException("siteId argument must have value or SiteId in Web.config file!");
            }
        }

        private Request CreateRequestObject(GlobalGamingDTO data)
        {
            var request = new Request
            {
                TimeStamp = TimeStamp,
                Auth = CreateAuthObject(),
                Call = CreateCallObject(data)
            };

            return request;
        }

        private Auth CreateAuthObject()
        {
            var auth = new Auth
            {
                Login = _configs.Login,
                Password = _configs.Password,
                SiteId = SiteId
            };

            return auth;
        }

        private Call CreateCallObject(GlobalGamingDTO data)
        {
            var languageCode = GetLanguageCode(data.Language);

            var call = new Call
            {
                Seq = Seq,
                ObCustId = data.CustId,
                GameId = data.GameId,
                Style = _configs.Style,
                Language = languageCode
            };

            return call;
        }

        private void ValidateResponse(Response response, Request request)
        {
            const int SuccessStatus = 0;
            DateTime requestDateTime;
            DateTime responseDateTime;

            DateTime.TryParse(TimeStamp, out requestDateTime);
            DateTime.TryParse(response.TimeStamp, out responseDateTime);

            var isEqualTimeStamp = requestDateTime.Equals(responseDateTime);

            if ((DebugMode)_configs.DebugMode == DebugMode.None)
            {
                if (!isEqualTimeStamp || response.Result.Seq != Seq)
                {
                    throw new GlobalGamingException("Send request incorrectly!");
                }

                if (response.Result.ErrorCode != SuccessStatus)
                {
                    throw new GlobalGamingException("From 3rd API - " + response.Result.ErrorDescription);
                }
            }
            else
            {
                throw new GlobalGamingException($"Send request incorrectly! Request: {SerializeJsonObject(request)}; Response: {SerializeJsonObject(response)}");
            }
        }
    }
}