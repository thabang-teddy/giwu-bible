using AutoMapper;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                    BibleId = id,
                    BibleName = bible.Name,
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
        
        public async Task<IActionResult> EditChapters(Guid id)
        {
            var bible = _repository.Bible.Get(x => x.Id == id, "BibleBook");
            if (bible == null) return NotFound();

            var biblebooks = _mapper.Map<BibleBooksViewModel>(bible.BibleBook);

            ChapterViewModel model = new()
            {
                BibleId = biblebooks.BibleId,
                BibleBookId = biblebooks.Id,
                BibleName = bible.Name,
                BookList = _mapper.Map<List<BibleBookViewModel>>(biblebooks.BookList),
            };
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

        [HttpGet("[Controller]/GetChapterCreatePartialForm/{bibleBookId}/{book}")]
        public IActionResult GetChapterCreatePartialForm(Guid bibleBookId,int book)
        {
            if (bibleBookId == Guid.Empty || book == 0)
            {
                string html = "<div class='alert alert-danger'>Data Error : Chapter could not be found!</div>";
                return Content(html, "text/html");
            }

            var model = new ChapterCreateFormViewModel() {
                Book = book,
                Number = 0,
                BibleBookId = bibleBookId,
                Verses = "",
            };

            return PartialView("/Areas/Admin/Views/Bibles/Partial/ChapterCreateForm.cshtml", model);
        }

		[HttpPost]
		public async Task<IActionResult> ChapterCreate(ChapterCreateFormViewModel model)
		{
			if (ModelState.IsValid)
			{
				// Save to DB or handle as needed

				if (model.BibleBookId == Guid.Empty || model.Book == 0 || model.Number == 0)
				{
					string html = "<div class='alert alert-danger'>Data Error : Chapter could not be found!</div>";
					return Content(html, "text/html");
				}

                try
				{
					var bibleBookInDb = _repository.BibleBook.Get(x => x.Id == model.BibleBookId);
					if (bibleBookInDb == null)
					{
						string html = "<div class='alert alert-danger'>Chapter could not be found!</div>";
						return Content(html, "text/html");
					}

					var bibleBookModel = _mapper.Map<BibleBooksViewModel>(bibleBookInDb);
					var newBookList = bibleBookModel.BookList.Select(x =>
                    {
                        if (x.Book == model.Book)
                        {
                            x.ChapterCount = x.ChapterCount + 1;
                        }

                        return x;
                    });

					Chapter newChapter = new() {
                        BibleBookId = model.BibleBookId,
		                Book = model.Book,
		                Number = model.Number,
		                Verses = model.Verses,
	                };

                    bibleBookInDb.BookList = JsonConvert.SerializeObject(newBookList);

					_repository.Chapter.Add(newChapter);
					_repository.BibleBook.Update(bibleBookInDb);
					await _repository.SaveChangesAsync();

					// Return plain HTML success message or a partial if preferred
					return Content("<div class='alert alert-success'>Chapter created successfully!, pleas refresh the chapter list if you want to see it.</div>", "text/html");
                }
                catch (Exception)
                {

                }

			}

			// If invalid, return validation messages as HTML (optional)
            return PartialView("/Areas/Admin/Views/Bibles/Partial/ChapterCreateForm.cshtml", model);
		}

		[HttpGet("[Controller]/GetChapterEditPartialForm/{bibleBookId}/{book}/{chapter}")]
        public IActionResult GetChapterEditPartialForm(Guid bibleBookId,int book,int chapter)
        {
            if (bibleBookId == Guid.Empty || book == 0 || chapter == 0)
            {
                string html = "<div class='alert alert-danger'>Data Error : Chapter could not be found!</div>";
                return Content(html, "text/html");
            }

            var chapterInDb = _repository.Chapter.Get(x => x.BibleBookId == bibleBookId &&  x.Book == book && x.Number == chapter);
            if (chapterInDb == null)
            {
                string html = "<div class='alert alert-danger'>Chapter could not be found!</div>";
                return Content(html, "text/html");
            }

            var model = new ChapterEditFormViewModel() {
                Id = chapterInDb.Id,
                Book = chapterInDb.Book,
                Number = chapterInDb.Number,
                Verses = chapterInDb.Verses,
            };

            return PartialView("/Areas/Admin/Views/Bibles/Partial/ChapterEditForm.cshtml", model);
        }

		[HttpPost]
		public async Task<IActionResult> ChapterEdit(ChapterEditFormViewModel model)
		{
			if (ModelState.IsValid)
			{
				// Save to DB or handle as needed

				if (model.Id == Guid.Empty || model.Book == 0 || model.Number == 0)
				{
					string html = "<div class='alert alert-danger'>Data Error : Chapter could not be found!</div>";
					return Content(html, "text/html");
				}

				try
				{
					var chapterInDb = _repository.Chapter.Get(x => x.Id == model.Id);
					if (chapterInDb == null)
					{
						string html = "<div class='alert alert-danger'>Chapter could not be found!</div>";
						return Content(html, "text/html");
					}

                    chapterInDb.Book = model.Book;
					chapterInDb.Number = model.Number;
					chapterInDb.Verses = model.Verses;

					_repository.Chapter.Update(chapterInDb);
					await _repository.SaveChangesAsync();

					// Return plain HTML success message or a partial if preferred
					return Content("<div class='alert alert-success'>Chapter Updated successfully!, pleas refresh the chapter list if you want to see it.</div>", "text/html");
				}
				catch (Exception)
				{

				}

			}

			// If invalid, return validation messages as HTML (optional)
			return PartialView("/Areas/Admin/Views/Bibles/Partial/ChapterEditForm.cshtml", model);
		}

	}
}
