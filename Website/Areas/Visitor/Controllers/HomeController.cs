using AutoMapper;
using Azure.Core;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
using System.Diagnostics;
using Website.Services;
using Website.ViewModels;
using Website.ViewModels.Visitor;
using static System.Formats.Asn1.AsnWriter;

namespace Website.Areas.Visitor.Controllers
{
    [Area("Visitor")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[Controller]/read/{bible}/{book}/{chapter}")]
        public async Task<IActionResult> Read(string bible, int book, int chapter)
        {
            var selectedBibleBook = await _unitOfWork.Bible.GetRow()
                .Where(x => x.Abbreviation.ToLower() == bible)
                .Include(x => x.BibleBook)
                .FirstOrDefaultAsync();

            if (selectedBibleBook != null && selectedBibleBook.BibleBook != null)
            {
                var selectedChapter = await _unitOfWork.Chapter.GetAsync(x => x.BibleBookId == selectedBibleBook.BibleBook.Id && x.Number == chapter);

                if (selectedChapter != null)
                {
                    var bookList = JsonConvert.DeserializeObject<List<Admin.ViewModels.Visitor.BibleBookViewModel>>(selectedBibleBook.BibleBook.BookList);
                    var verses = JsonConvert.DeserializeObject<List<VersesViewModel>>(selectedChapter.Verses);

                    if (verses != null && verses.Any())
                    {
                        VisitorChapterViewModel viewModels = new()
                        {
                            Bible = selectedBibleBook.Name,
                            Book = bookList?.Where(x => x.Book == book).FirstOrDefault()?.Name ?? null,
                            Number = chapter,
                            Verses = verses
                        };

                        return View(viewModels);
                    }
                }
            }

            return RedirectToAction("ChapterNotFound");
        }
        
        public IActionResult ChapterNotFound()
        {
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
