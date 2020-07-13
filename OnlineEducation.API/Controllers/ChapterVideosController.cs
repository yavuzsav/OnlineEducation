using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.API.Helper;
using OnlineEducation.Business.Handlers.ChapterVideoHandlers.Commands;

namespace OnlineEducation.API.Controllers
{
    public class ChapterVideosController : BaseController
    {
        [RequestSizeLimit(250000000)]
        [HttpPost("{id}")]
        public async Task<ActionResult<Unit>> UploadVideo(Guid id, [FromForm] VideoFile videoFile)
        {
            return await Mediator.Send(new CreateChapterVideo.Command {ChapterId = id, File = videoFile.File});
        }

        [HttpDelete("{chapterId}/videos/{chapterVideoId}")]
        public async Task<ActionResult<Unit>> DeleteVideo(Guid chapterId, Guid chapterVideoId)
        {
            return await Mediator.Send(new DeleteChapterVideo.Command
                {ChapterId = chapterId, ChapterVideoId = chapterVideoId});
        }
    }
}
