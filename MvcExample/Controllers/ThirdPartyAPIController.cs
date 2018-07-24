namespace MvcExample.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Models.GlobalGaming;
    using MvcExample.Models.Voidbridge;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ThirdPartyAPIController : ApiController
    {
        private const string Login = "Login";
        private const string Password = "LoginSecret";

        private static Dictionary<int, string> ErrorDescriptions = new Dictionary<int, string>
        {
            {0, "Success" },
            {1, "Login failed" },
            {2, "Have no data" }
        };

        [HttpPost]
        public JObject GetGlobalGamingResultUrl([FromBody] GlobalGamingRequest request)
        {
            var errorCode = request.Auth.Login == Login && request.Auth.Password == Password ? 0 : 1;
            errorCode = errorCode == 0 && int.Parse(request.Call.GameId) > 1010 ? 2 : 0;
            var response = new GlobalGamingResponse
            {
                TimeStamp = request.TimeStamp,
                Result = new Models.GlobalGaming.Result
                {
                    Seq = request.Call.Seq,
                    ObCustId = request.Call.ObCustId,
                    GameId = request.Call.GameId,
                    ResultUrl = "https://nexdev.net/projects/gg-gaming/wiki",
                    ErrorCode = errorCode,
                    ErrorDescription = ErrorDescriptions[errorCode]
                }
            };

            return JObject.Parse(JsonConvert.SerializeObject(response));
        }

        [HttpPost]
        public JObject GetVoidBridgeUrl([FromBody] Request request)
        {
            var response = new Response
            {
                TimeStamp = request.TimeStamp,
                Result = new Models.Voidbridge.Result
                {
                    Seq = request.Call.Seq,
                    Url = "https://nexdev.net/projects/voidbridge/wiki/Single_Wallet_BetList",
                    ErrorCode = "0"
                }
            };

            return JObject.Parse(JsonConvert.SerializeObject(response));
        }

        [HttpPost]
        public JObject GetPPResultUrl([FromBody] Request request)
        {
            var response = new Response
            {
                TimeStamp = request.TimeStamp,
                Result = new Models.Voidbridge.Result
                {
                    Seq = request.Call.Seq,
                    Url = "https://nexdev.net/projects/p111023/wiki/201801_Pragmatic_Betlist",
                    ErrorCode = "0"
                }
            };

            return JObject.Parse(JsonConvert.SerializeObject(response));
        }
    }
}