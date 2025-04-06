using AutoMapper;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using Website.Areas.Admin.ViewModels.Bibles;

namespace Website.Areas.Admin.Controllers
{
    public class BiblesController : Controller
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public BiblesController(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var bibles = _repository.Bible.GetAll();
            var model = _mapper.Map<List<BibleViewModel>>(bibles);
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BibleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var bible = _mapper.Map<Bible>(model);
                bible.Id = Guid.NewGuid();
                _repository.Bible.Add(bible);
                _repository.Save();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var bible = _repository.Bible.Get(x => x.Id == id);
            if (bible == null) return NotFound();

            var model = _mapper.Map<BibleViewModel>(bible);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BibleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var bible = _mapper.Map<Bible>(model);
                    _repository.Bible.Update(bible);
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
            var bible = _repository.Bible.Get(x => x.Id == id);
            if (bible == null) return NotFound();

            var model = _mapper.Map<BibleViewModel>(bible);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var bible = _repository.Bible.Get(x => x.Id == id);
            _repository.Bible.Remove(bible);
            _repository.Save();
            return RedirectToAction("Index");
        }
    }
}
