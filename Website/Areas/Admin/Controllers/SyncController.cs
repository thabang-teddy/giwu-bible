using AutoMapper;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using SQLitePCL;

namespace Website.Areas.Admin.Controllers
{
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
            List<Bible> bibles = new List<Bible>();
            List<BibleBook> bibleBooks = new List<BibleBook>();
            List<Chapter> chapters = new List<Chapter>();


            var bibleVersions = await _sqlite.BibleVersionKeys.ToListAsync();

            var abbreviations = await _sqlite.KeyAbbreviationsEnglish.ToListAsync();

            var keys = await _sqlite.KeyEnglish.ToListAsync();

            var genres = await _sqlite.KeyGenreEnglish.ToListAsync();

            var verses = await _sqlite.T_Asv.ToListAsync();


            if (bibleVersions != null && bibleVersions.Any())
            {
                for (int i = 0; i < bibleVersions.Count; i++)
                {
                    Bible bible = new()
                    {
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
                    };
                    
                }
            }

            //await _mysql.SaveChangesAsync();

            return View();
        }
    }
}
