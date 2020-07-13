using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.Core.Interfaces;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Handlers.ChapterVideoHandlers.Commands
{
    public class DeleteChapterVideo
    {
        public class Command : IRequest
        {
            public Guid ChapterId { get; set; }
            public Guid ChapterVideoId { get; set; }
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
                var chapter = await chapterRepository.GetByIdAsync(request.ChapterId);

                if (chapter == null) throw new RestException(HttpStatusCode.NotFound);

                var chapterVideo = chapter.ChapterVideos.FirstOrDefault(x => x.Id == request.ChapterVideoId);
                var result = chapterVideo != null && await _videoService.DeleteVideoAsync(chapterVideo.PublicId);

                if (!result) throw new Exception(ExceptionMessages.ProblemDeletingVideo);

                chapter.ChapterVideos.Remove(chapterVideo);

                var success = await _unitOfWork.CompleteAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception(ExceptionMessages.ProblemSavingChanges);
            }
        }
    }
}
