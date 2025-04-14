using AutoMapper;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.SyncModels.BibleSQlite;
using Newtonsoft.Json;
using SQLitePCL;

namespace Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SyncController : Controller
    {
        private readonly IUnitOfWork _repository;
        private readonly SqliteDbContext _sqlite;

        public SyncController(IUnitOfWork repository, SqliteDbContext sqlite)
        {
            _repository = repository;
            _sqlite = sqlite;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SQliteOne()
         {
            try
            {
                List<Bible> bibles = new List<Bible>();

                var bibleVersions = await _sqlite.BibleVersionKeys.ToListAsync();

                var keys = await _sqlite.KeyEnglish.ToListAsync();

                if (bibleVersions != null && bibleVersions.Any())
                {
                    for (int i = 0; i < bibleVersions.Count; i++)
                    {
                        List<Verses> verses = new();

                        if (bibleVersions[i].table == "t_asv")
                        {
                            verses = _sqlite.T_Asv.ToList();
                        }
                        else if (bibleVersions[i].table == "t_bbe")
                        {
                            verses = _sqlite.T_Bbe.ToList();
                        }
                        else if (bibleVersions[i].table == "t_kjv")
                        {
                            verses = _sqlite.T_Kjv.ToList();
                        }
                        else if (bibleVersions[i].table == "t_web")
                        {
                            verses = _sqlite.T_Web.ToList();
                        }
                        else if (bibleVersions[i].table == "t_ylt")
                        {
                            verses = _sqlite.T_Ylt.ToList();
                        }

                        var bibleId = Guid.NewGuid();
                        var bibleBooksId = Guid.NewGuid();

                        List<Chapter> BookChapterList = verses.GroupBy(x => new { x.b, x.c }).Select(chapter => {
                            return new Chapter()
                            {
                                Id = Guid.NewGuid(),
                                BibleBookId = bibleBooksId,
                                Book = chapter.Key.b,
                                Number = chapter.Key.c,
                                Verses = JsonConvert.SerializeObject(chapter.Select(v => new {Verse = v.v, Text = v.t }).ToList())
                            };
                        }).ToList();

                        var bibleBookList = keys.Select( book => new ViewModels.Visitor.BibleBookViewModel()
                        {
                            Book = book.b,
                            Name = book.n,
                            ChapterCount = BookChapterList.Where(x => x.Book == book.b).Count(),
                        }).ToList();

                        BibleBook bibleBooks = new()
                        {
                            Id = bibleBooksId,
                            BibleId = bibleId,
                            BookList = JsonConvert.SerializeObject(bibleBookList),
                            Chapters = BookChapterList
                        };

                        Bible bible = new()
                        {
                            Id = bibleId,
                            LagacyId = bibleVersions[i].id.ToString(),
                            RootTable = bibleVersions[i].table,
                            RootUrl = "https://github.com/thabang-teddy/bible_databases",
                            Name = bibleVersions[i].version,
                            Abbreviation = bibleVersions[i].abbreviation,
                            About = "",
                            Url = bibleVersions[i].info_url,
                            Publisher = bibleVersions[i].publisher,
                            Copyright = bibleVersions[i].copyright,
                            Language = bibleVersions[i].language,
                            OtherInfo = "",
                            BibleBook = bibleBooks
                        };

                        bibles.Add(bible);
                    
                    }
                }


                await _repository.Bible.AddRangeAsync(bibles);
                await _repository.SaveChangesAsync();

                return View();

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
