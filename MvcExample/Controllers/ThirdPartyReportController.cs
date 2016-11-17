namespace MvcExample.Controllers
{
    using System;
    using System.Web.Mvc;
    using Fanex.Result.Core.GlobalGaming;
    using Fanex.Result.Core.GlobalGaming.DTO;
    using Fanex.Result.Core.Voidbridge;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ThirdPartyReportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string VoidbridgeResult(string custId, string gamePlayId, string language)
        {
            try
            {
                var voidbridgeApi = new VoidbridgeApiImp();
                return voidbridgeApi.GetBetHistoryUrl(custId, gamePlayId, language, "12");
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public JObject GlobalGamingResult(string custId, string gameId, string language)
        {
            var error = 0;
            var content = string.Empty;
            try
            {
                var globalGamingApi = new GlobalGamingResultApi();
                content = globalGamingApi.GetResultUrl(new GlobalGamingDTO
                {
                    CustId = custId,
                    GameId = gameId,
                    Language = language
                });
            }
            catch (Exception e)
            {
                error = 1;
                content = e.ToString();
            }

            return JObject.Parse(JsonConvert.SerializeObject(new { Error = error, Url = content }));
        }
    }
}