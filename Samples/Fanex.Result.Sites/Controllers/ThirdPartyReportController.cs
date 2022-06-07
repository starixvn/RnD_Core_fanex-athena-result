namespace MvcExample.Controllers
{
    using System;
    using System.Web.Mvc;
    using Fanex.Result.Core.CardClub;
    using Fanex.Result.Core.Casino;
    using Fanex.Result.Core.GlobalGaming;
    using Fanex.Result.Core.GlobalGaming.DTO;
    using Fanex.Result.Core.PP;
    using Fanex.Result.Core.Voidbridge;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ThirdPartyReportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string VoidbridgeResult(string custId, string gamePlayId, string language)
        {
            try
            {
                var voidbridgeApi = new VoidbridgeApiImp();
                return voidbridgeApi.GetBetHistoryUrl(custId, gamePlayId, language, "12");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet]
        public JObject GlobalGamingResult(GlobalGamingDTO globalGamingData)
        {
            var error = 0;
            var content = string.Empty;
            try
            {
                var globalGamingApi = new GlobalGamingResultApi();
                content = globalGamingApi.GetResultUrl(globalGamingData);
            }
            catch (Exception e)
            {
                error = 1;
                content = e.Message;
            }

            return JObject.Parse(JsonConvert.SerializeObject(new { Error = error, Url = content }));
        }

        [HttpGet]
        public string PPResult(string custId, string gamePlayId, string language)
        {
            try
            {
                var ppResult = new PPResult();
                return ppResult.GetPPResult(custId, gamePlayId, language, "12");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        [HttpGet]
        public string CardClubResult(string custId, string gameTypeId, string transId3rd, string language)
        {
            try
            {
                var cardClubResult = new CardClubResult();
                return cardClubResult.GetResult(custId, gameTypeId, transId3rd, language).Replace("http://", "https://");
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        [HttpGet]
        public string SpadegamingResult(string custId, string gamePlayId, string language)
        {
            try
            {
                var sgResult = new CasinoResult("SpadegamingResultConfigs");
                return sgResult.GetCasinoResult(custId, gamePlayId, language);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}