namespace MvcExample.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using Models.Voidbridge;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class RNGSingleLobbyController : ApiController
    {
        [HttpPost]
        public JObject GetBetHistoryUrl([FromBody] Request request)
        {
            var response = new Response { TimeStamp = request.TimeStamp, Result = new Result { Seq = request.Call.Seq, Url = "http://nexdev.net", ErrorCode = "0" } };

            return JObject.Parse(JsonConvert.SerializeObject(response));
        }

        private string GetCurrentIp()
        {
            string host = Dns.GetHostName();
            IPHostEntry ips = Dns.GetHostEntry(host);
            var ip = ips.AddressList.LastOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();

            return "http://" + ip;
        }
    }
}