namespace MvcExample.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Models.GlobalGaming;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class GlobalGamingInternalController : ApiController
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
        public JObject GetResultUrl([FromBody] GlobalGamingRequest request)
        {
            var errorCode = request.Auth.Login == Login && request.Auth.Password == Password ? 0 : 1;
            errorCode = errorCode == 0 && int.Parse(request.Call.GameId) > 1010 ? 2 : 0;
            var response = new GlobalGamingResponse
            {
                TimeStamp = request.TimeStamp,
                Result = new Result
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
    }
}