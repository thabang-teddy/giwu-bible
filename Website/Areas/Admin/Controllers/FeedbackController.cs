using Microsoft.AspNetCore.Mvc;

namespace Website.Areas.Admin.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
