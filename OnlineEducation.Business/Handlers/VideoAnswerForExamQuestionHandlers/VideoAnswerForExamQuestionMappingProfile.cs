using AutoMapper;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.VideoAnswerForExamQuestionHandlers
{
    public class VideoAnswerForExamQuestionMappingProfile : Profile
    {
        public VideoAnswerForExamQuestionMappingProfile()
        {
            CreateMap<VideoAnswerForExamQuestion, VideoAnswerForExamQuestionDto>();
        }
    }
}
