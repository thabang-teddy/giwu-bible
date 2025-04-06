using AutoMapper;
using Models;
using Website.Areas.Admin.ViewModels.Bibles;

namespace Website.Areas.Admin.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Bible Mapping
            CreateMap<Bible, BibleViewModel>().ReverseMap();
            CreateMap<BibleBook, BibleBookViewModel>().ReverseMap();
            CreateMap<Chapter, ChapterViewModel>().ReverseMap();
        }
    }
}
