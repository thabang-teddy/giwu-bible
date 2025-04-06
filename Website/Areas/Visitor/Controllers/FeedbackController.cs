using AutoMapper;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Website.ViewModels.Visitor;

namespace Website.Areas.Visitor.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public FeedbackController(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(FeedbackViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var feedback = _mapper.Map<Feedback>(model);
                feedback.Id = Guid.NewGuid();
                _repository.Feedback.Add(feedback);
                _repository.Save();
                return RedirectToAction("FeedbackSuccess");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        public async Task<IActionResult> FeedbackSuccess()
        {
            return View();
        }
    }
}
