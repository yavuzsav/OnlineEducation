using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineEducation.Business.Specifications.ChapterSpecifications;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.Core.Interfaces;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.ChapterVideoHandlers.Commands
{
    public class CreateChapterVideo
    {
        public class Command : IRequest
        {
            public Guid ChapterId { get; set; }
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
                var chapterRepository = _unitOfWork.Repository<Chapter>();

                var spec = new ChapterWithChapterVideosAndLessonSpecification(request.ChapterId);
                var chapter = await chapterRepository.GetEntityWithSpecificationAsync(spec);
                if (chapter == null) throw new RestException(HttpStatusCode.NotFound, "Chapter not found");

                var videoUploadResult = await _videoService.UploadVideoAsync(request.File);

                var chapterVideo = new ChapterVideo
                {
                    PublicId = videoUploadResult.PublicId,
                    Url = videoUploadResult.Url,
                    CreatedAt = videoUploadResult.CreatedAt,
                };

                chapter.ChapterVideos.Add(chapterVideo);

                var success = await _unitOfWork.CompleteAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception(ExceptionMessages.ProblemSavingChanges);
            }
        }
    }
}
