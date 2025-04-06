using AutoMapper;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Website.Areas.Admin.ViewModels.Feedback;

namespace Website.Areas.Admin.Controllers
{
    public class FeedbacksController : Controller
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public FeedbacksController(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var bibles = _repository.Feedback.GetAll();
            var model = _mapper.Map<List<FeedbackViewModel>>(bibles);
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var bible = _repository.Feedback.Get(x => x.Id == id);
            if (bible == null) return NotFound();

            var model = _mapper.Map<FeedbackEditViewModel>(bible);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FeedbackEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var bible = _repository.Feedback.Get(x => x.Id == model.Id);

                    bible.Notes = model.Notes;
                    bible.AlertDate = model.AlertDate;
                    bible.UpdateAt = DateTime.Now;
                    bible.UpdateBy = "update";

                    _repository.Feedback.Update(bible);
                    _repository.Save();
                    return RedirectToAction("Edit", new { id = bible.Id });
                }
                catch (Exception ex)
                {
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var bible = _repository.Feedback.Get(x => x.Id == id);
            if (bible == null) return NotFound();

            var model = _mapper.Map<FeedbackViewModel>(bible);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bible = _repository.Feedback.Get(x => x.Id == id);
            _repository.Feedback.Remove(bible);
            _repository.Save();
            return RedirectToAction("Index");
        }
    }
}
