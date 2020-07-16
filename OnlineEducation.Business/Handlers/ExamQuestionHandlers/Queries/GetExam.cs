using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OnlineEducation.Business.Specifications.ChapterSpecifications;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.ExamQuestionHandlers.Queries
{
    public class GetExam
    {
        public class Query : IRequest<IReadOnlyList<ExamQuestionDto>>
        {
            public Guid ChapterId { get; set; }
            public int Count { get; set; } = 20;
        }

        public class Handler : IRequestHandler<Query, IReadOnlyList<ExamQuestionDto>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
            }

            public async Task<IReadOnlyList<ExamQuestionDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var chapterRepository = _unitOfWork.Repository<Chapter>();
                var chapterSpec = new ChaptersWithQuestionsSpecification(request.ChapterId);

                var questions = chapterRepository.ListWithSpecificationAsync(chapterSpec).Result
                    .SelectMany(x => x.ExamQuestions).OrderBy(x => Guid.NewGuid()).Take(request.Count)
                    .ToImmutableList();

                var data = _mapper.Map<IReadOnlyList<ExamQuestion>, IReadOnlyList<ExamQuestionDto>>(questions);
                return data;
            }
        }
    }
}
