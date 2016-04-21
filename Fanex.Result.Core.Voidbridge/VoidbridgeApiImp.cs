namespace Fanex.Result.Core.Voidbridge
{
    using System;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using Exceptions;
    using Models;

    public class VoidbridgeApiImp : VoidbridgeApi
    {
        private VoidbridgeConfigs _configs;
        private string _timeStamp;
        private string _seq;

        public VoidbridgeApiImp()
        {
            _configs = VoidbridgeConfigs.Configs;
            _timeStamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
        }

        public string GetBetHistoryUrl(string custId, string gamePlayId, bool debugMode = false)
        {
            if (ValidateArguments(custId, gamePlayId))
            {
                var jsonData = CreateJsonData(custId, gamePlayId);

                if (debugMode)
                {
                    return jsonData;
                }
                else
                {
                    var response = SendRequest(jsonData);
                    return GetUrlFromResponse(response);
                }
            }

            return string.Empty;
        }

        private bool ValidateArguments(string custId, string gamePlayId)
        {
            if (string.IsNullOrEmpty(custId))
            {
                throw new VoidbridgeException("custId is null or empty!");
            }

            if (string.IsNullOrEmpty(gamePlayId))
            {
                throw new VoidbridgeException("gamePlayId is null or empty!");
            }

            return true;
        }

        private string CreateJsonData(string custId, string gamePlayId)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Request));

            var request = CreateRequestObject(custId, gamePlayId);

            ser.WriteObject(stream, request);

            stream.Position = 0;
            var streamReader = new StreamReader(stream);

            return streamReader.ReadToEnd();
        }

        private Request CreateRequestObject(string custId, string gamePlayId)
        {
            var request = new Request();

            request.TimeStamp = _timeStamp;
            request.Auth = CreateAuthObject();
            request.Call = CreateCallObject(custId, gamePlayId);

            return request;
        }

        private Auth CreateAuthObject()
        {
            var auth = new Auth();

            auth.Login = _configs.Login;
            auth.Password = _configs.Password;
            auth.SiteId = _configs.SiteId;

            return auth;
        }

        private Call CreateCallObject(string custId, string gamePlayId)
        {
            var call = new Call();

            GenerateSeqCode(custId, gamePlayId);

            call.Seq = _seq;
            call.OBCustId = custId;
            call.GamePlayId = gamePlayId;
            call.VendorId = _configs.VendorId;

            return call;
        }

        private void GenerateSeqCode(string custId, string gamePlayId)
        {
            var random = new Random();

            var timestampWithRandom = $"{DateTime.Now.Ticks}-{random.Next(0, int.MaxValue)}";

            var hashedPreInfo = $"{_configs.SiteId}-{custId}-{gamePlayId}-{random.Next(0, int.MaxValue)}";

            _seq = $"{timestampWithRandom}-{hashedPreInfo}";
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

        private string GetUrlFromResponse(string response)
        {
            var responseObject = DeserializeResponse(response);

            var url = ValidateResponse(responseObject)
                        ? responseObject.Result.Url
                        : string.Empty;

            return url;
        }

        private Response DeserializeResponse(string response)
        {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(response));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Response));

            return (Response)ser.ReadObject(stream);
        }

        private bool ValidateResponse(Response responseObject)
        {
            var successStatus = "0";

            if (responseObject.TimeStamp != _timeStamp || responseObject.Result.Seq != _seq)
            {
                throw new VoidbridgeException("Send request incorrectly!");
            }

            if (responseObject.Result.ErrorCode != successStatus)
            {
                throw new VoidbridgeException(responseObject.Result.ErrorDescription);
            }

            return true;
        }
    }
}