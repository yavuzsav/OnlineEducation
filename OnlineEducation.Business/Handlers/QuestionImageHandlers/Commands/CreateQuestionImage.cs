using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.Core.Interfaces;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.QuestionImageHandlers.Commands
{
    public class CreateQuestionImage
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
                var question = await questionRepository.GetByIdAsync(request.QuestionId);

                if (question == null) throw new RestException(HttpStatusCode.NotFound, "Question not fount");

                var imageUploadResult = await _imageService.UploadImageAsync(request.File, "OnlineEducation/QuestionImage");

                var questionImage = new QuestionImage
                {
                    Url = imageUploadResult.Url,
                    PublicId = imageUploadResult.PublicId,
                    QuestionId = request.QuestionId
                };

                var questionImageRepository = _unitOfWork.Repository<QuestionImage>();
                questionImageRepository.Add(questionImage);

                var result = await _unitOfWork.CompleteAsync() > 0;
                if (result) return Unit.Value;

                throw new Exception(ExceptionMessages.ProblemSavingChanges);
            }
        }
    }
}
