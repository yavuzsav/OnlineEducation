using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineEducation.Business.Specifications.QuestionSpecifications;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.Core.Interfaces;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.AnswerHandlers.Commands
{
    public class CreateAnswerImageForAnswer
    {
        public class Command : IRequest
        {
            public Guid QuestionId { get; set; }
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IImageService _imageService;

            public Handler(IUnitOfWork unitOfWork, IImageService imageService)
            {
                _unitOfWork = unitOfWork;
                _imageService = imageService;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var questionRepository = _unitOfWork.Repository<Question>();
                var questionWithAnswerSpecification = new QuestionWithAnswerSpecification(request.QuestionId);
                var question =
                    await questionRepository.GetEntityWithSpecificationAsync(questionWithAnswerSpecification);

                if (question == null) throw new RestException(HttpStatusCode.NotFound, "Question not found");

                if (question.IsAnswerVideo)
                    throw new RestException(HttpStatusCode.BadRequest, "Answer should not be image");

                if (question.IsAnswerVideo && question.Answer.AnswerVideo != null)
                    throw new RestException(HttpStatusCode.BadRequest, "Answer already exists");

                var uploadResult = await _imageService.UploadImageAsync(request.File, "OnlineEducation/AnswerImages");

                var answerImage = new AnswerImage
                {
                    Url = uploadResult.Url,
                    PublicId = uploadResult.PublicId,
                    CreatedAt = uploadResult.CreatedAt,
                };

                question.Answer.AnswerImages.Add(answerImage);

                var result = await _unitOfWork.CompleteAsync() > 0;
                if (result) return Unit.Value;

                throw new Exception(ExceptionMessages.ProblemSavingChanges);
            }
        }
    }
}
