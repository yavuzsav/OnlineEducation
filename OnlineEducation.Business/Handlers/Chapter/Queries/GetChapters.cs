using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnlineEducation.Business.Specifications.ChapterSpecifications;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Dtos;

namespace OnlineEducation.Business.Handlers.Chapter.Queries
{
    public class GetChapters
    {
        public class Query : IRequest<Pagination<ChapterDto>>
        {
            public PaginationParams PaginationParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Pagination<ChapterDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Pagination<ChapterDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var chapterRepository = _unitOfWork.Repository<Entities.Entities.Chapter>();

                var chapterSpecification = new ChapterSpecification(request.PaginationParams);
                var chapters = await chapterRepository.ListWithSpecificationAsync(chapterSpecification);
                var count = await chapterRepository.CountAsync(new ChapterSpecification());

                var mappedData =
                    _mapper.Map<IReadOnlyList<Entities.Entities.Chapter>, IReadOnlyList<ChapterDto>>(chapters);

                return new Pagination<ChapterDto>(request.PaginationParams.PageIndex, request.PaginationParams.PageSize,
                    count, mappedData);
            }
        }
    }
}
