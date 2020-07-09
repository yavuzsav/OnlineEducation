using AutoMapper;
using OnlineEducation.Entities.Dtos;

namespace OnlineEducation.Business.Handlers.Category
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Entities.Entities.Category, CategoryDto>()
                .ForMember(dest => dest.Lessons, opt => opt.MapFrom(src => src.Lessons));
        }
    }
}
