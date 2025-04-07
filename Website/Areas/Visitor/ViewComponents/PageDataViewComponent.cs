using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Website.Services;
using Website.ViewModels.Visitor;

namespace Website.Areas.Visitor.ViewComponents
{
    public class PageDataViewComponent : ViewComponent
    {
        private readonly AppDataService _appDataService; // Inject DB context if needed

        public PageDataViewComponent (AppDataService appDataService)
        {
            _appDataService = appDataService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<BibleViewModel> biblesViewModel = _appDataService.GetGlogalBibleList();

            PageDataViewModel pageDataViewModel = new()
            {
                Book = 0,
                chapter = 0,
            };

            if (biblesViewModel.Any())
            {
                pageDataViewModel.Bible = biblesViewModel[0]?.Id;
                pageDataViewModel.BibleList = JsonConvert.SerializeObject(biblesViewModel);
            }
            else
            {
                pageDataViewModel.Bible = Guid.Empty;
                pageDataViewModel.BibleList = "[]";
            }

            return View(pageDataViewModel);
        }
    }
}
