using AutoMapper;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.QuestionImageHandlers
{
    public class QuestionImageMappingProfile : Profile
    {
        public QuestionImageMappingProfile()
        {
            CreateMap<QuestionImage, QuestionImageDto>();
        }
    }
}
