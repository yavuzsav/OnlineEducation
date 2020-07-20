using AutoMapper;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.ExamQuestionHandlers
{
    public class ExamQuestionMappingProfile : Profile
    {
        public ExamQuestionMappingProfile()
        {
            CreateMap<VideoAnswerForExamQuestion, VideoAnswerForExamQuestionDto>();
            CreateMap<ExamQuestion, ExamQuestionDto>()
                .ForMember(dest => dest.VideoAnswerForExamQuestions,
                    opt => opt.MapFrom(src => src.VideoAnswerForExamQuestion));
        }
    }
}
