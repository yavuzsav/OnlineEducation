using AutoMapper;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.ChapterHandlers
{
    public class ChapterMappingProfile : Profile
    {
        public ChapterMappingProfile()
        {
            CreateMap<Chapter, ChapterDto>()
                .ForMember(dest => dest.LessonName, opt => opt.MapFrom(src => src.Lesson.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Lesson.Category.Name));

            CreateMap<Chapter, ChapterWithChapterVideosDto>();
        }
    }
}
