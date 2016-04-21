namespace MvcExample.Controllers
{
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
            var voidbridgeApi = new VoidbridgeApiImp();

            return voidbridgeApi.GetBetHistoryUrl(custId, gamePlayId);
        }
    }
}