using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnlineEducation.Business.Specifications.LessonSpecifications;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.DataAccess.Interfaces;

namespace OnlineEducation.Business.Handlers.Lesson.Queries
{
    public class GetLessonsForCategoryId
    {
        public class Query : IRequest<Pagination<Entities.Entities.Lesson>>
        {
            public Guid CategoryId { get; set; }
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
                var categoryRepository = _unitOfWork.Repository<Entities.Entities.Category>();

                var category = await categoryRepository.GetByIdAsync(request.CategoryId);
                if (category == null) throw new RestException(HttpStatusCode.NotFound);

                var lessonByCategorySpec =
                    new LessonByCategorySpecification(request.CategoryId, request.PaginationParams);
                var lessons = await lessonRepository.ListWithSpecificationAsync(lessonByCategorySpec);
                var count = await lessonRepository.CountAsync(new LessonByCategorySpecification(request.CategoryId));

                return new Pagination<Entities.Entities.Lesson>(request.PaginationParams.PageIndex,
                    request.PaginationParams.PageSize, count, lessons);
            }
        }
    }
}
