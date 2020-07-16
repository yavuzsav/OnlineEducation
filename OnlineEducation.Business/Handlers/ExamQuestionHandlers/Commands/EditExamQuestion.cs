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
    public class EditExamQuestion
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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
                var examQuestionRepository = _unitOfWork.Repository<ExamQuestion>();
                var examQuestion = await examQuestionRepository.GetByIdAsync(request.Id);

                if (examQuestion == null) throw new RestException(HttpStatusCode.NotFound);

                var chapterRepository = _unitOfWork.Repository<Chapter>();
                var chapter = await chapterRepository.GetByIdAsync(request.ChapterId);

                if (chapter == null) throw new RestException(HttpStatusCode.NotFound, "Chapter not found");

                examQuestion.ChapterId = request.ChapterId;
                examQuestion.Content = request.Content ?? examQuestion.Content;
                examQuestion.Option1 = request.Option1 ?? examQuestion.Option1;
                examQuestion.Option2 = request.Option2 ?? examQuestion.Option2;
                examQuestion.Option3 = request.Option3 ?? examQuestion.Option3;
                examQuestion.Option4 = request.Option4 ?? examQuestion.Option4;
                examQuestion.CorrectAnswer = request.CorrectAnswer;

                var success = await _unitOfWork.CompleteAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception(ExceptionMessages.ProblemSavingChanges);
            }
        }
    }
}
