using AutoMapper;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.CategoryHandlers
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryWithLessonsDto>()
                .ForMember(dest => dest.Lessons, opt => opt.MapFrom(src => src.Lessons));

            CreateMap<Category, CategoryDto>();
        }
    }
}
