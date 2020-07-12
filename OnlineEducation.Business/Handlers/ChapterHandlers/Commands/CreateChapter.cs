using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.ChapterHandlers.Commands
{
    public class CreateChapter
    {
        public class Command : IRequest
        {
            public Guid LessonId { get; set; }
            public string Name { get; set; }
            public string Content { get; set; }
            public string Description { get; set; }
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
                var lessonRepository = _unitOfWork.Repository<Lesson>();

                var lesson = await lessonRepository.GetByIdAsync(request.LessonId);
                if (lesson == null) throw new RestException(HttpStatusCode.NotFound, "Lesson not found");

                var chapterRepository = _unitOfWork.Repository<Chapter>();
                var chapter = new Chapter
                {
                    LessonId = request.LessonId,
                    Name = request.Name,
                    Content = request.Content,
                    Description = request.Description
                };

                chapterRepository.Add(chapter);

                var success = await _unitOfWork.CompleteAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception(ExceptionMessages.ProblemSavingChanges);
            }
        }
    }
}
