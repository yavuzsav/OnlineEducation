using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnlineEducation.Business.Specifications.CategorySpecifications;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.CategoryHandlers.Queries
{
    public class GetCategoryWithLessons
    {
        public class Query : IRequest<CategoryDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, CategoryDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<CategoryDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var categoryRepository = _unitOfWork.Repository<Category>();

                var categoryWithLessonsSpec = new CategoryWithLessonsSpecification(request.Id);
                var category = await categoryRepository.GetEntityWithSpecificationAsync(categoryWithLessonsSpec);

                if (category == null) throw new RestException(HttpStatusCode.NotFound);

                return _mapper.Map<Category, CategoryDto>(category);
            }
        }
    }
}
