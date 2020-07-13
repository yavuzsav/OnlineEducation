using AutoMapper;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.ChapterVideoHandlers
{
    public class ChapterVideoMappingProfile : Profile
    {
        public ChapterVideoMappingProfile()
        {
            CreateMap<ChapterVideo, ChapterVideoDto>();
        }
    }
}
