using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.Core.Interfaces;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Entities;
using OnlineEducation.Entities.Identity;

namespace OnlineEducation.Business.Handlers.QuestionHandlers.Commands
{
    public class CreateQuestion
    {
        public class Command : IRequest
        {
            public string Message { get; set; }
            public DateTime? CreatedAt { get; set; }
            public bool IsAnswerVideo { get; set; }
            public Guid OwnerId { get; set; }
            public Guid LessonId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IUserAccessor _userAccessor;
            private readonly UserManager<AppUser> _userManager;

            public Handler(IUnitOfWork unitOfWork, IUserAccessor userAccessor, UserManager<AppUser> userManager)
            {
                _unitOfWork = unitOfWork;
                _userAccessor = userAccessor;
                _userManager = userManager;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = _userAccessor.GetCurrentUserId();
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null) throw new RestException(HttpStatusCode.Unauthorized);

                if (!await _userManager.IsInRoleAsync(user, "Student")) throw new RestException(HttpStatusCode.Unauthorized, "User is not student");

                var lessonRepository = _unitOfWork.Repository<Lesson>();
                var lesson = await lessonRepository.GetByIdAsync(request.LessonId);

                if (lesson == null) throw new RestException(HttpStatusCode.NotFound, "Lesson not found");

                var question = new Question
                {
                    Message = request.Message,
                    CreatedAt = request.CreatedAt ?? DateTime.Now,
                    IsAnswerVideo = request.IsAnswerVideo,
                    LessonId = request.LessonId,
                    OwnerId = user.Id,
                };

                var questionRepository = _unitOfWork.Repository<Question>();
                questionRepository.Add(question);

                var result = await _unitOfWork.CompleteAsync() > 0;
                if (result) return Unit.Value;

                throw new Exception(ExceptionMessages.ProblemSavingChanges);
            }
        }
    }
}
