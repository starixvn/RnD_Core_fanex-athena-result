namespace MvcExample.Controllers
{
    using System;
    using System.Web.Mvc;
    using Fanex.Result.Core.Voidbridge;

    public class ThirdPartyReportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string VoidbridgeResult(string custId, string gamePlayId)
        {
            try
            {
                var voidbridgeApi = new VoidbridgeApiImp();
                return voidbridgeApi.GetBetHistoryUrl(custId, gamePlayId, "12");
            }
            catch (Exception e) {
                return e.ToString();
            }
        }
    }
}