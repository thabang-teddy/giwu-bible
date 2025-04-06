using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Website.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SyncController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
