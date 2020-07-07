using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnlineEducation.Business.Specifications.CategorySpecifications;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.DataAccess.Interfaces;

namespace OnlineEducation.Business.Handlers.Category.Queries
{
    public class CategoryList
    {
        public class Query : IRequest<Pagination<Entities.Entities.Category>>
        {
            public PaginationParams PaginationParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Pagination<Entities.Entities.Category>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Pagination<Entities.Entities.Category>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var categoryRepository = _unitOfWork.Repository<Entities.Entities.Category>();

                var categorySpec = new CategorySpecification(request.PaginationParams);

                var categories = await categoryRepository.ListWithSpecificationAsync(categorySpec);
                var count = await categoryRepository.CountAsync(new CategorySpecification());

                return new Pagination<Entities.Entities.Category>(request.PaginationParams.PageIndex,
                    request.PaginationParams.PageSize, count, categories);
            }
        }
    }
}
