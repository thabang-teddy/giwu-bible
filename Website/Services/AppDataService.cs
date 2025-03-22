using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Models;
using Newtonsoft.Json;
using Website.ViewModels.Visitor;

namespace Website.Services
{
    public class AppDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<BibleViewModel> GlogalBibleList { get; set; }
        public AppDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            GlogalBibleList = GenerateBiblelist();
        }

        private List<BibleViewModel> GenerateBiblelist()
        {
            List<BibleViewModel> bibleList = new();

            var biblesInDb = _unitOfWork.Bible.GetAll().ToList();
            var bibleBooksInDb = _unitOfWork.BibleBook.GetAll().ToList();

            if (biblesInDb.Any())
            {
                for (int i = 0; i < biblesInDb.Count; i++)
                {
                    var bible = new BibleViewModel()
                    {
                        Id = biblesInDb[i].Id,
                        Name = biblesInDb[i].Name,
                        Abbreviation = biblesInDb[i].Abbreviation,
                        About = biblesInDb[i].About,
                        Url = biblesInDb[i].Url,
                        Publisher = biblesInDb[i].Publisher,
                        Copyright = biblesInDb[i].Copyright,
                        Language = biblesInDb[i].Language,
                        OtherInfo = biblesInDb[i].OtherInfo,
                    };

                    var bibleBooks = bibleBooksInDb.Where(x => x.BibleId == biblesInDb[i].Id).ToList();

                    if (bibleBooks.Any())
                    {
                        for (int a = 0; a < bibleBooks.Count; a++)
                        {
                            var bibleBook = new BibleBookViewModel()
                            {
                                Name = bibleBooks[a].Name,
                                ChapterCount = bibleBooks[a].ChapterCount
                            };
                        }
                    }

                    bibleList.Add(bible);

                }
            }

            return bibleList;
        }

        public void RegenerateBiblelist()
        {
            GlogalBibleList = GenerateBiblelist();
        }

        public List<BibleViewModel> GetGlogalBibleList()
        {
            return GlogalBibleList;
        }
    }
}
