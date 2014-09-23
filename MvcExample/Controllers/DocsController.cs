namespace MvcExample.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// DOCS controller.
    /// </summary>
    public class DocsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}