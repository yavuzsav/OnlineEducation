using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnlineEducation.Business.Specifications.ExamQuestionSpecifications;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.Core.Interfaces;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.VideoAnswerForExamQuestionHandlers.Commands
{
    public class DeleteVideoAnswerForExamQuestion
    {
        public class Command : IRequest
        {
            public Guid ExamQuestionId { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IVideoService _videoService;

            public Handler(IUnitOfWork unitOfWork, IVideoService videoService)
            {
                _unitOfWork = unitOfWork;
                _videoService = videoService;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var examQuestionRepository = _unitOfWork.Repository<ExamQuestion>();
                var videoAnswerForExamQuestionRepository = _unitOfWork.Repository<VideoAnswerForExamQuestion>();

                var examQuestionWithVideosSpecification =
                    new ExamQuestionWithVideosSpecification(request.ExamQuestionId);

                var examQuestion =
                    await examQuestionRepository.GetEntityWithSpecificationAsync(examQuestionWithVideosSpecification);

                if (examQuestion == null) throw new RestException(HttpStatusCode.NotFound, "Exam question not found");

                if (examQuestion.VideoAnswerForExamQuestion == null)
                    return Unit.Value;

                var deleteResult =
                    await _videoService.DeleteVideoAsync(examQuestion.VideoAnswerForExamQuestion.PublicId);

                if (!deleteResult) throw new Exception(ExceptionMessages.ProblemDeletingVideo);

                examQuestion.VideoAnswerForExamQuestion = null;
                videoAnswerForExamQuestionRepository.Delete(examQuestion.VideoAnswerForExamQuestion);

                var success = await _unitOfWork.CompleteAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception(ExceptionMessages.ProblemSavingChanges);
            }
        }
    }
}
