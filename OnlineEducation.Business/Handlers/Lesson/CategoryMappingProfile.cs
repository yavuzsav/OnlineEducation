using AutoMapper;
using OnlineEducation.Entities.Dtos;

namespace OnlineEducation.Business.Handlers.Lesson
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Entities.Entities.Lesson, LessonDto>();
        }
    }
}
