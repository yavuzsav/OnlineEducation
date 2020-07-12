using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.LessonHandlers.Commands
{
    public class EditLesson
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public Guid CategoryId { get; set; }
            public string Name { get; set; }
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
                var categoryRepository = _unitOfWork.Repository<Category>();
                var lessonRepository = _unitOfWork.Repository<Lesson>();

                var lesson = await lessonRepository.GetByIdAsync(request.Id);
                if (lesson == null)
                    throw new RestException(HttpStatusCode.NotFound, "Lesson not found");

                var category = await categoryRepository.GetByIdAsync(request.CategoryId);
                if (category == null)
                    throw new RestException(HttpStatusCode.NotFound, "Category not found");

                lesson.CategoryId = request.CategoryId;
                lesson.Name = request.Name ?? lesson.Name;
                lesson.Description = request.Description ?? lesson.Description;

                var success = await _unitOfWork.CompleteAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception(ExceptionMessages.ProblemSavingChanges);
            }
        }
    }
}
