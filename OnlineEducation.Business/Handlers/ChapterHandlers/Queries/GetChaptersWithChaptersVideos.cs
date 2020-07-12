using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnlineEducation.Business.Specifications.ChapterSpecifications;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.ChapterHandlers.Queries
{
    public class GetChaptersWithChaptersVideos
    {
        public class Query : IRequest<Pagination<ChapterWithChapterVideosDto>>
        {
            public PaginationParams PaginationParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Pagination<ChapterWithChapterVideosDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Pagination<ChapterWithChapterVideosDto>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var chapterRepository = _unitOfWork.Repository<Chapter>();

                var spec = new ChapterWithChapterVideosAndLessonSpecification(request.PaginationParams);
                var chapters = await chapterRepository.ListWithSpecificationAsync(spec);
                var count = await chapterRepository.CountAsync(new ChapterWithChapterVideosAndLessonSpecification());

                var mappedData =
                    _mapper.Map<IReadOnlyList<Chapter>, IReadOnlyList<ChapterWithChapterVideosDto>>(
                        chapters);

                return new Pagination<ChapterWithChapterVideosDto>(request.PaginationParams.PageIndex,
                    request.PaginationParams.PageSize, count, mappedData);
            }
        }
    }
}
