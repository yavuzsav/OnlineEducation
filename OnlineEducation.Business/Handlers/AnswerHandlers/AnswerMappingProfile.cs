using AutoMapper;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.AnswerHandlers
{
    public class AnswerMappingProfile : Profile
    {
        public AnswerMappingProfile()
        {
            CreateMap<Answer, AnswerDto>();
            CreateMap<AnswerVideo, AnswerVideoDto>();
            CreateMap<AnswerImage, AnswerImageDto>();
        }
    }
}
