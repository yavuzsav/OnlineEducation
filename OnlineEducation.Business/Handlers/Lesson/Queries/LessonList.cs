using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnlineEducation.Business.Specifications.LessonSpecifications;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.DataAccess.Interfaces;

namespace OnlineEducation.Business.Handlers.Lesson.Queries
{
    public class LessonList
    {
        public class Query : IRequest<Pagination<Entities.Entities.Lesson>>
        {
            public PaginationParams PaginationParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Pagination<Entities.Entities.Lesson>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Pagination<Entities.Entities.Lesson>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var lessonRepository = _unitOfWork.Repository<Entities.Entities.Lesson>();

                var lessonSpec = new LessonSpecification(request.PaginationParams);
                var lessons = await lessonRepository.ListWithSpecificationAsync(lessonSpec);
                var count = await lessonRepository.CountAsync(new LessonSpecification());

                return new Pagination<Entities.Entities.Lesson>(request.PaginationParams.PageIndex,
                    request.PaginationParams.PageSize, count, lessons);
            }
        }
    }
}
