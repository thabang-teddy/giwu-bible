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

            CreateMap<BibleBook, BibleBooksViewModel>()
                .ForMember(dest => dest.BookList, opt => opt.MapFrom(src => AdminBibleBookToList(src.BookList)));
            
            CreateMap<BibleBooksViewModel, BibleBook>()
                .ForMember(dest => dest.BookList, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.BookList)));

            //CreateMap<Bible, VisitorBibleViewModel>()
            //.ForMember(dest => dest.BibleBooks, opt => opt.MapFrom(src => BibleBookToList(src.BibleBook.BookList)));
            CreateMap<Bible, VisitorBibleViewModel>()
                .ForMember(dest => dest.BibleBooks, opt =>
                    opt.MapFrom(src => src.BibleBook != null ? BibleBookToList(src.BibleBook.BookList) : new List<VisitorBibleBookViewModel>())
                );
        }

        /// <summary>
        /// Converts a JSON string inside a BibleBook to a list of BibleBookViewModel
        /// </summary>
        public static List<BibleBookViewModel> AdminBibleBookToList(string bookList)
        {
            if (string.IsNullOrEmpty(bookList))
                return new List<BibleBookViewModel>();

            try
            {
                return JsonConvert.DeserializeObject<List<BibleBookViewModel>>(bookList)
                       ?? new List<BibleBookViewModel>();
            }
            catch (JsonException)
            {
                // Log error if needed
                return new List<BibleBookViewModel>();
            }
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
