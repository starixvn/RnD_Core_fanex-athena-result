using Microsoft.AspNetCore.Mvc;

namespace Fanex.Result.Sites.NET8.Controllers;

/// <summary>
/// DOCS controller.
/// </summary>
public class DocsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
