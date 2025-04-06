using AutoMapper;
using Models;
using Website.Areas.Admin.ViewModels.Bibles;

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
        }
    }
}
