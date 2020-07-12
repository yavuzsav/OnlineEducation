using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnlineEducation.Business.Specifications.ChapterSpecifications;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.ChapterHandlers.Queries
{
    public class GetChapterDetails
    {
        public class Query : IRequest<ChapterWithChapterVideosDto>
        {
            public Guid ChapterId { get; set; }
        }

        public class Handler : IRequestHandler<Query, ChapterWithChapterVideosDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<ChapterWithChapterVideosDto> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var chapterRepository = _unitOfWork.Repository<Chapter>();

                var spec = new ChapterWithChapterVideosAndLessonSpecification(request.ChapterId);
                var chapters = await chapterRepository.GetEntityWithSpecificationAsync(spec);
                var count = await chapterRepository.CountAsync(new ChapterWithChapterVideosAndLessonSpecification());

                var mappedData =
                    _mapper.Map<Chapter, ChapterWithChapterVideosDto>(
                        chapters);

                return mappedData;
            }
        }
    }
}
