using AutoMapper;
using OnlineEducation.Entities.Dtos;

namespace OnlineEducation.Business.Handlers.Chapter
{
    public class ChapterMappingProfile : Profile
    {
        public ChapterMappingProfile()
        {
            CreateMap<Entities.Entities.Chapter, ChapterDto>()
                .ForMember(dest => dest.LessonName, opt => opt.MapFrom(src => src.Lesson.Name));

            CreateMap<Entities.Entities.Chapter, ChapterWithChapterVideosDto>();
        }
    }
}
