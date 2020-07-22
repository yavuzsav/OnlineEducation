using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineEducation.Business.Specifications.ExamQuestionSpecifications;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.Core.Interfaces;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.VideoAnswerForExamQuestionHandlers.Commands
{
    public class CreateVideoAnswerForExamQuestion
    {
        public class Command : IRequest
        {
            public Guid ExamQuestionId { get; set; }
            public IFormFile File { get; set; }
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
                var examQuestionWithVideosSpecification =
                    new ExamQuestionWithVideosSpecification(request.ExamQuestionId);
                var examQuestion =
                    await examQuestionRepository.GetEntityWithSpecificationAsync(examQuestionWithVideosSpecification);

                if (examQuestion == null) throw new RestException(HttpStatusCode.NotFound, "Exam question not found");

                var videoUploadResult =
                    await _videoService.UploadVideoAsync(request.File, "OnlineEducation/VideoAnswerForExamQuestion");

                var videoAnswerForExamQuestion = new VideoAnswerForExamQuestion
                {
                    Url = videoUploadResult.Url,
                    CreatedAt = videoUploadResult.CreatedAt,
                    PublicId = videoUploadResult.PublicId,
                };
                examQuestion.VideoAnswerForExamQuestion = videoAnswerForExamQuestion;

                var success = await _unitOfWork.CompleteAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception(ExceptionMessages.ProblemSavingChanges);
            }
        }
    }
}
