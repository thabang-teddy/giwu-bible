using AutoMapper;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using Website.Areas.Admin.ViewModels.Dashboard;

namespace Website.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _repository;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(
            IUnitOfWork repository,
            UserManager<ApplicationUser> userManager
        )
        {
            _repository = repository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var dashboard = new DashboardViewModel();
            dashboard.Bibles = _repository.Bible.GetRow().Count();
            dashboard.Syncs = 0;
            dashboard.Feedbacks = _repository.Feedback.GetRow().Count();
            dashboard.Users = _userManager.Users.Count();
            return View(dashboard);
        }
    }
}
