using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.DataAccess.Interfaces;

namespace OnlineEducation.Business.Handlers.Chapter.Commands
{
    public class EditChapter
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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
                var chapterRepository = _unitOfWork.Repository<Entities.Entities.Chapter>();
                var chapter = await chapterRepository.GetByIdAsync(request.Id);

                var lessonRepository = _unitOfWork.Repository<Entities.Entities.Lesson>();
                var lesson = await lessonRepository.GetByIdAsync(request.LessonId);
                if (lesson == null) throw new RestException(HttpStatusCode.NotFound, "Lesson not found");

                chapter.LessonId = request.LessonId;
                chapter.Name = request.Name ?? chapter.Name;
                chapter.Content = request.Content ?? chapter.Content;
                chapter.Description = request.Description ?? chapter.Description;

                var success = await _unitOfWork.CompleteAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception(ExceptionMessages.ProblemSavingChanges);
            }
        }
    }
}
