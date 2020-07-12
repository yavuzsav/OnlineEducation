using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnlineEducation.Business.Specifications.CategorySpecifications;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.CategoryHandlers.Queries
{
    public class CategoryList
    {
        public class Query : IRequest<Pagination<Category>>
        {
            public PaginationParams PaginationParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Pagination<Category>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Pagination<Category>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var categoryRepository = _unitOfWork.Repository<Category>();

                var categorySpec = new CategorySpecification(request.PaginationParams);

                var categories = await categoryRepository.ListWithSpecificationAsync(categorySpec);
                var count = await categoryRepository.CountAsync(new CategorySpecification());

                return new Pagination<Category>(request.PaginationParams.PageIndex,
                    request.PaginationParams.PageSize, count, categories);
            }
        }
    }
}
