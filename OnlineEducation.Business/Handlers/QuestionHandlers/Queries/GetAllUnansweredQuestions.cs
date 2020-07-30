using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineEducation.Business.Specifications.QuestionSpecifications;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.Core.Interfaces;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Entities;
using OnlineEducation.Entities.Identity;

namespace OnlineEducation.Business.Handlers.QuestionHandlers.Queries
{
    public class GetAllUnansweredQuestions
    {
        public class Query : IRequest<Pagination<QuestionDto>>
        {
            public PaginationParams PaginationParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Pagination<QuestionDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<Pagination<QuestionDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var questionRepository = _unitOfWork.Repository<Question>();
                var questionsUnansweredSpecification = new QuestionsUnansweredSpecification(request.PaginationParams);
                var questions = await questionRepository.ListWithSpecificationAsync(questionsUnansweredSpecification);
                var count = await questionRepository.CountAsync(new QuestionsUnansweredSpecification());

                var mappedData = _mapper.Map<IReadOnlyList<Question>, IReadOnlyList<QuestionDto>>(questions);

                return new Pagination<QuestionDto>(request.PaginationParams.PageIndex,
                    request.PaginationParams.PageSize, count, mappedData);
            }
        }
    }
}
