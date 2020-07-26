using AutoMapper;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.QuestionHandlers
{
    public class QuestionMappingProfile : Profile
    {
        public QuestionMappingProfile()
        {
            CreateMap<Question, QuestionDto>()
                .ForMember(dest => dest.LessonName, opt => opt.MapFrom(src => src.Lesson.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Lesson.Category.Name))
                .ForMember(dest => dest.QuestionImageDto, opt => opt.MapFrom(src => src.QuestionImage));
        }
    }
}
