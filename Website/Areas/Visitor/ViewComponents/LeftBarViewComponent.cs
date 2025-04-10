using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Website.Services;
using Website.ViewModels.Visitor;

namespace Website.Areas.Visitor.ViewComponents
{
    public class LeftBarViewComponent : ViewComponent
    {
        private readonly AppDataService _appDataService;

        public LeftBarViewComponent(AppDataService appDataService)
        {
            _appDataService = appDataService;
        }

        public IViewComponentResult Invoke()
        {
            List<VisitorBibleViewModel> biblesViewModel = _appDataService.GetGlogalBibleList();

            return View(biblesViewModel);
        }
    }
}
