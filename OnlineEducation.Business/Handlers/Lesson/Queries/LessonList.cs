using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnlineEducation.Business.Specifications.LessonSpecifications;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Dtos;

namespace OnlineEducation.Business.Handlers.Lesson.Queries
{
    public class LessonList
    {
        public class Query : IRequest<Pagination<LessonWithCategoryNameDto>>
        {
            public PaginationParams PaginationParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Pagination<LessonWithCategoryNameDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Pagination<LessonWithCategoryNameDto>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var lessonRepository = _unitOfWork.Repository<Entities.Entities.Lesson>();

                var lessonSpec = new LessonSpecification(request.PaginationParams);
                var lessons = await lessonRepository.ListWithSpecificationAsync(lessonSpec);
                var count = await lessonRepository.CountAsync(new LessonSpecification());

                var mappedData =
                    _mapper.Map<IReadOnlyList<Entities.Entities.Lesson>, IReadOnlyList<LessonWithCategoryNameDto>>(
                        lessons);

                return new Pagination<LessonWithCategoryNameDto>(request.PaginationParams.PageIndex,
                    request.PaginationParams.PageSize, count, mappedData);
            }
        }
    }
}
