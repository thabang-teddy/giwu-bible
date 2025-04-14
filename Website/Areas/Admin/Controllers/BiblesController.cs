using AutoMapper;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Website.Areas.Admin.ViewModels.Bibles;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
        
        public async Task<IActionResult> EditBooks(Guid id)
        {
            var bible = _repository.Bible.Get(x => x.Id == id, "BibleBook");
            if (bible == null) return NotFound();

            var model = _mapper.Map<BibleBooksViewModel>(bible.BibleBook);
            if (model == null)
            {
                model = new()
                {
                    BibleId = id
                };
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditBooks(BibleBooksViewModel model)
        {
            if (ModelState.IsValid && model != null && model.BookList != null && model.BookList.Any())
            {
                try
                {
                    var bibleBooksInDb = _repository.BibleBook.Get(x => x.BibleId == model.BibleId);
                    if (bibleBooksInDb == null) {
                        BibleBook newBibleBooks = new()
                        {
                            Id = Guid.NewGuid(),
                            BibleId = model.BibleId,
                            BookList = JsonConvert.SerializeObject(model.BookList)
                        };
                        _repository.BibleBook.Add(newBibleBooks);
                    }
                    else
                    {
                        var bibleBooks = _mapper.Map<BibleBook>(model);

                        bibleBooksInDb.BookList = JsonConvert.SerializeObject(model.BookList);

                        _repository.BibleBook.Update(bibleBooksInDb);
                    }
                    await _repository.SaveChangesAsync();
                    return RedirectToAction("EditBooks", new { id = model.BibleId });
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
