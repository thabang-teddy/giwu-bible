using AutoMapper;
using Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using Website.Areas.Admin.ViewModels.Bibles;
using Website.ViewModels.Visitor;

namespace Website.Mapping
{
    public class BibleProfile : Profile
    {
        public BibleProfile()
        {
            // Bible Mapping
            CreateMap<Bible, BibleViewModel>().ReverseMap();
            CreateMap<BibleBook, BibleBookViewModel>().ReverseMap();
            CreateMap<Chapter, ChapterViewModel>().ReverseMap();


            //CreateMap<Bible, VisitorBibleViewModel>()
            //.ForMember(dest => dest.BobleBooks, opt => opt.MapFrom(src => BibleBookToList(src.BibleBook.BookList)));
            CreateMap<Bible, VisitorBibleViewModel>()
                .ForMember(dest => dest.BobleBooks, opt =>
                    opt.MapFrom(src => src.BibleBook != null ? BibleBookToList(src.BibleBook.BookList) : new List<VisitorBibleBookViewModel>())
                );
        }

        /// <summary>
        /// Converts a JSON string inside a BibleBook to a list of BibleBookViewModel
        /// </summary>
        public static List<VisitorBibleBookViewModel> BibleBookToList(string bookList)
        {
            if (string.IsNullOrEmpty(bookList))
                return new List<VisitorBibleBookViewModel>();

            try
            {
                return JsonConvert.DeserializeObject<List<VisitorBibleBookViewModel>>(bookList)
                       ?? new List<VisitorBibleBookViewModel>();
            }
            catch (JsonException)
            {
                // Log error if needed
                return new List<VisitorBibleBookViewModel>();
            }
        }
    }
}
