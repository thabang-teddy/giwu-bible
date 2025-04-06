using AutoMapper;
using Models;
using Website.Areas.Admin.ViewModels.Feedback;

namespace Website.Areas.Admin.Mapping
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            // Feedback Mapping
            CreateMap<Feedback, Website.ViewModels.Visitor.FeedbackViewModel>().ReverseMap();
            CreateMap<Feedback, FeedbackViewModel>().ReverseMap();
            CreateMap<Feedback, FeedbackEditViewModel>().ReverseMap();
        }
    }
}
