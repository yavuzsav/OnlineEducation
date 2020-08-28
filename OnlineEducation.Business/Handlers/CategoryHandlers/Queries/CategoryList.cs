using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnlineEducation.Business.Specifications.CategorySpecifications;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.CategoryHandlers.Queries
{
    public class CategoryList
    {
        public class Query : IRequest<Pagination<CategoryDto>>
        {
            public PaginationParams PaginationParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Pagination<CategoryDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
                _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
            }

            public async Task<Pagination<CategoryDto>> Handle(Query request,
                CancellationToken cancellationToken)
            {
                var categoryRepository = _unitOfWork.Repository<Category>();

                var categorySpec = new CategorySpecification(request.PaginationParams);

                var categories = await categoryRepository.ListWithSpecificationAsync(categorySpec);
                var count = await categoryRepository.CountAsync(new CategorySpecification());

                var mappedData = _mapper.Map<IReadOnlyList<CategoryDto>>(categories);

                return new Pagination<CategoryDto>(request.PaginationParams.PageIndex,
                    request.PaginationParams.PageSize, count, mappedData);
            }
        }
    }
}