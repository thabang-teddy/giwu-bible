using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Website.ViewModels.Visitor;

namespace Website.Services
{
    public class BibleService
    {
        private readonly HttpClient _http;
        private readonly IUnitOfWork _unitOfWork;

        public BibleService(HttpClient http, IUnitOfWork unitOfWork)
        {
            _http = http;
            _unitOfWork = unitOfWork;
        }

        public async Task<VisitorChapterViewModel> GetChapter(
            Guid bibleId,
            int book,
            int chapter
        )
        {
        //        var selectedChapter = await _unitOfWork.Chapter.GetAsync(x => x.BibleBookId == selectedBibleBook.BibleBook.Id && x.Number == chapter);
            //var result = await _unitOfWork.Bible.GetRow()
            var result = await _unitOfWork.Chapter.GetRow()
                .Where(x => x.BibleBookId == bibleId && x.Book == book && x.Number == chapter)
                .Include(c => c.BibleBook)
                .ThenInclude(bb => bb.Bible)
                .Select(c => new VisitorChapterViewModel
                {
                    Bible = c.BibleBook.Bible.Name,
                    Book = c.BibleBook.BookList,
                    Number = c.Number,
                    Verses = JsonSerializer.Deserialize<List<VersesViewModel>>(
                        c.Verses,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }) ?? new()
                })
                .FirstOrDefaultAsync();

            return result!;
        }

        public async Task<VersesViewModel> GetVerse(string book, int chapter, int verse, string version)
        {
            var url = $"https://bible-api.com/{book}+{chapter}:{verse}?translation={version}";
            var data = await _http.GetFromJsonAsync<VisitorChapterViewModel>(url);
            return data.Verses.First();
        }
    }
}
