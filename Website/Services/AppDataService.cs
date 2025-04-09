using AutoMapper;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using Website.ViewModels.Visitor;

namespace Website.Services
{
    public class AppDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        public List<VisitorBibleViewModel> GlogalBibleList { get; set; }
        private readonly IMapper _mapper;

        public AppDataService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            GlogalBibleList = GenerateBiblelist();

            _mapper = mapper;
        }

        private List<VisitorBibleViewModel> GenerateBiblelist()
        {
            var bibleList = _unitOfWork.Bible
                .GetRow()
                .Include(x => x.BibleBook).ToList();

            if (bibleList == null || !bibleList.Any())
            {
                return new List<VisitorBibleViewModel>();
            }

            //var returndata = _mapper.Map<List<VisitorBibleViewModel>>(bibleList);
            return bibleList.Select(x => {
                List<VisitorBibleBookViewModel> bobleBooks = new();

                if (x.BibleBook != null && !string.IsNullOrEmpty(x.BibleBook.BookList))
                {
                    bobleBooks = JsonConvert.DeserializeObject<List<VisitorBibleBookViewModel>>(x.BibleBook.BookList);
                }

                return new VisitorBibleViewModel() {
                    Id = x.Id,
                    Name = x.Name,
                    Abbreviation = x.Abbreviation,
                    About = x.About,
                    Url = x.Url,
                    Publisher = x.Publisher,
                    Copyright = x.Copyright,
                    Language = x.Language,
                    OtherInfo = x.OtherInfo,
                    BobleBooks = bobleBooks
                };
            }).ToList();
        }

        public void RegenerateBiblelist()
        {
            GlogalBibleList = GenerateBiblelist();
        }

        public List<VisitorBibleViewModel> GetGlogalBibleList()
        {
            return GlogalBibleList;
        }
    }
}
