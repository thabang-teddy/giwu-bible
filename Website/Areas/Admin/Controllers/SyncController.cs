using Microsoft.AspNetCore.Mvc;

namespace Website.Areas.Admin.Controllers
{
    public class SyncController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
