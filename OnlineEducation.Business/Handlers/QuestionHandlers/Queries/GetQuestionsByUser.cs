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
    public class GetQuestionsByUser
    {
        public class Query : IRequest<Pagination<QuestionDto>>
        {
            public PaginationParams PaginationParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Pagination<QuestionDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;
            private readonly UserManager<AppUser> _userManager;

            public Handler(IUnitOfWork unitOfWork, IUserAccessor userAccessor, IMapper mapper,
                UserManager<AppUser> userManager)
            {
                _unitOfWork = unitOfWork;
                _userAccessor = userAccessor;
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<Pagination<QuestionDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = _userAccessor.GetCurrentUserId();
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null) throw new RestException(HttpStatusCode.Unauthorized);

                var questionRepository = _unitOfWork.Repository<Question>();
                var questionWithLessonSpecification =
                    new QuestionsWithLessonSpecification(userId, request.PaginationParams);
                var questions = await questionRepository.ListWithSpecificationAsync(questionWithLessonSpecification);
                var count = await questionRepository.CountAsync(new QuestionsWithLessonSpecification(userId));

                var mappedData = _mapper.Map<IReadOnlyList<Question>, IReadOnlyList<QuestionDto>>(questions);

                return new Pagination<QuestionDto>(request.PaginationParams.PageIndex,
                    request.PaginationParams.PageSize, count, mappedData);
            }
        }
    }
}
