namespace MvcExample.Controllers
{
    using System.Web.Mvc;
    using ThirdParty.Remoting;
    using ThirdParty;
    using System;

    /// <summary>
    /// Home controller.
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
           
        }
    }
}