using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnlineEducation.Business.Specifications.ChapterSpecifications;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.ChapterHandlers.Queries
{
    public class GetChapterQuestions
    {
        public class Query : IRequest<Pagination<ExamQuestionDto>>
        {
            public Guid ChapterId { get; set; }
            public PaginationParams PaginationParams { get; set; }
        }

        public class Handler : IRequestHandler<Query, Pagination<ExamQuestionDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

#pragma warning disable 1998
            public async Task<Pagination<ExamQuestionDto>> Handle(Query request, CancellationToken cancellationToken)
#pragma warning restore 1998
            {
                var chapterRepository = _unitOfWork.Repository<Chapter>();
                var specification = new ChapterWithQuestionsSpecification(request.ChapterId, request.PaginationParams);

                var questions = chapterRepository.GetEntityWithSpecificationAsync(specification).Result
                    .ExamQuestions
                    .Skip(specification.Skip)
                    .Take(specification.Take)
                    .ToList();

                if (questions == null) throw new RestException(HttpStatusCode.NotFound, "Questions not found");

                var countSpecification = new ChapterWithQuestionsSpecification(request.ChapterId);
                var count = chapterRepository.GetEntityWithSpecificationAsync(countSpecification).Result.ExamQuestions
                    .Count;

                var mappedData = _mapper.Map<IReadOnlyList<ExamQuestion>, IReadOnlyList<ExamQuestionDto>>(questions);

                return new Pagination<ExamQuestionDto>(request.PaginationParams.PageIndex,
                    request.PaginationParams.PageSize, count, mappedData);
            }
        }
    }
}
