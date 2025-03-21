using DataAcess.Data;
using Microsoft.AspNetCore.Mvc;

namespace Website.Areas.Visitor.ViewComponents
{
    public class LeftBarViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context; // Inject DB context if needed

        public LeftBarViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
