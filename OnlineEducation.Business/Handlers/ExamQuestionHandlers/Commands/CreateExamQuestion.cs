using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Entities;
using OnlineEducation.Entities.Enums;

namespace OnlineEducation.Business.Handlers.ExamQuestionHandlers.Commands
{
    public class CreateExamQuestion
    {
        public class Command : IRequest
        {
            public Guid ChapterId { get; set; }
            public string Content { get; set; }
            public string Option1 { get; set; }
            public string Option2 { get; set; }
            public string Option3 { get; set; }
            public string Option4 { get; set; }
            public CorrectAnswer CorrectAnswer { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var chapterRepository = _unitOfWork.Repository<Chapter>();
                var chapter = await chapterRepository.GetByIdAsync(request.ChapterId);

                if (chapter == null) throw new RestException(HttpStatusCode.NotFound, "Chapter not found");

                var examQuestionRepository = _unitOfWork.Repository<ExamQuestion>();

                var examQuestion = new ExamQuestion
                {
                    ChapterId = request.ChapterId,
                    Content = request.Content,
                    Option1 = request.Option1,
                    Option2 = request.Option2,
                    Option3 = request.Option3,
                    Option4 = request.Option4,
                    CorrectAnswer = request.CorrectAnswer
                };

                examQuestionRepository.Add(examQuestion);

                var success = await _unitOfWork.CompleteAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception(ExceptionMessages.ProblemSavingChanges);
            }
        }
    }
}
