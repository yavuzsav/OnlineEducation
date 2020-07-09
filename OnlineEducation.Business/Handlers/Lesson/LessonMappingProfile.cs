using AutoMapper;
using OnlineEducation.Entities.Dtos;

namespace OnlineEducation.Business.Handlers.Lesson
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Entities.Entities.Lesson, LessonDto>();

            CreateMap<Entities.Entities.Lesson, LessonWithCategoryNameDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
