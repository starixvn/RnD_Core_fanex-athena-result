namespace Fanex.Result.Core.Voidbridge
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using Exceptions;
    using Models;

    public class VoidbridgeApiImp : VoidbridgeApi
    {
        private const string DefaultLanguage = "en-US";

        private static readonly Dictionary<string, string> SupportedLanguage = new Dictionary<string, string>
        {
            { "en-US", "en" },
            { "zh-CN", "cs" },
            { "zh-Hans", "zhcn" },
            { "th-TH", "th" },
            { "vi-VN", "vn" },
            { "id-ID", "id" }
        };

        private VoidbridgeConfigs _configs;
        private string _timeStamp;
        private string _seq;
        private string _siteId;

        public VoidbridgeApiImp()
        {
            _configs = VoidbridgeConfigs.Configs;
            _timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        public string GetBetHistoryUrl(string custId, string gamePlayId, string language = DefaultLanguage, string siteId = null)
        {
            ValidateArguments(custId, gamePlayId, siteId);
            _siteId = siteId ?? _configs.SiteId;

            var url = string.Empty;
            var jsonRequest = CreateJsonData(custId, gamePlayId, language);

            if ((DebugMode)_configs.DebugMode == DebugMode.DontSendRequest)
            {
                return jsonRequest;
            }
            else
            {
                var response = SendRequest(jsonRequest);

                var parsedResponse = DeserializeResponse(response);

                if (ValidateResponse(parsedResponse, jsonRequest))
                {
                    url = parsedResponse.Result.Url;
                }
            }

            return url;
        }

        private bool ValidateArguments(string custId, string gamePlayId, string siteId)
        {
            if (string.IsNullOrEmpty(custId))
            {
                throw new VoidbridgeException("custId is null or empty!");
            }

            if (string.IsNullOrEmpty(gamePlayId))
            {
                throw new VoidbridgeException("gamePlayId is null or empty!");
            }

            if (string.IsNullOrEmpty(siteId) && string.IsNullOrEmpty(_configs.SiteId))
            {
                throw new VoidbridgeException("siteId argument must have value or SiteId in Web.config file!");
            }

            return true;
        }

        private string CreateJsonData(string custId, string gamePlayId, string language)
        {
            var request = CreateRequestObject(custId, gamePlayId, language);

            return SerializeRequest(request);
        }

        private Request CreateRequestObject(string custId, string gamePlayId, string language)
        {
            var request = new Request();

            request.TimeStamp = _timeStamp;
            request.Auth = CreateAuthObject();
            request.Call = CreateCallObject(custId, gamePlayId, language);

            return request;
        }

        private Auth CreateAuthObject()
        {
            var auth = new Auth();

            auth.Login = _configs.Login;
            auth.Password = _configs.Password;
            auth.SiteId = _siteId;

            return auth;
        }

        private Call CreateCallObject(string custId, string gamePlayId, string language)
        {
            var call = new Call();
            var languageCode = string.IsNullOrEmpty(language) || !SupportedLanguage.ContainsKey(language)
                                ? SupportedLanguage[DefaultLanguage]
                                : SupportedLanguage[language];

            GenerateSeqCode(custId, gamePlayId);

            call.Seq = _seq;
            call.OBCustId = custId;
            call.GamePlayId = gamePlayId;
            call.VendorId = _configs.VendorId;
            call.Language = languageCode;

            return call;
        }

        private void GenerateSeqCode(string custId, string gamePlayId)
        {
            var random = new Random();

            var timestampWithRandom = $"{DateTime.Now.Ticks}-{random.Next(0, int.MaxValue)}";

            var hashedPreInfo = $"{_configs.SiteId}-{custId}-{gamePlayId}-{random.Next(0, int.MaxValue)}";

            _seq = $"{timestampWithRandom}-{hashedPreInfo}";
        }

        private string SerializeRequest(Request request)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Request));

            return SerializeObject(ser, request);
        }

        private string SerializeObject(DataContractJsonSerializer ser, object request)
        {
            MemoryStream stream = new MemoryStream();

            ser.WriteObject(stream, request);

            stream.Position = 0;
            var streamReader = new StreamReader(stream);

            return streamReader.ReadToEnd();
        }

        private string SendRequest(string jsonData)
        {
            var result = string.Empty;

            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                result = client.UploadString(_configs.Url, jsonData);
            }

            return result;
        }

        private Response DeserializeResponse(string response)
        {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(response));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Response));

            return (Response)ser.ReadObject(stream);
        }

        private bool ValidateResponse(Response responseObject, string request)
        {
            var successStatus = "0";
            var requestDateTime = new DateTime();
            var responseDateTime = new DateTime();

            DateTime.TryParse(_timeStamp, out requestDateTime);
            DateTime.TryParse(responseObject.TimeStamp, out responseDateTime);

            var isEqualTimeStamp = requestDateTime.Equals(responseDateTime);

            if (!isEqualTimeStamp || responseObject.Result.Seq != _seq || responseObject.Result.ErrorCode != successStatus)
            {
                if ((DebugMode)_configs.DebugMode == DebugMode.None)
                {
                    if (responseObject.Result.ErrorCode != successStatus)
                    {
                        throw new VoidbridgeException(responseObject.Result.ErrorDescription);
                    }
                    else
                    {
                        throw new VoidbridgeException("Send request incorrectly!");
                    }
                }
                else
                {
                    throw new VoidbridgeException($"Send request incorrectly! Detail request: {request}; Detail response: {SerializeResponse(responseObject)}");
                }
            }

            return true;
        }

        private string SerializeResponse(Response response)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Response));

            return SerializeObject(ser, response);
        }
    }
}