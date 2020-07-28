using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.API.Helper;
using OnlineEducation.Business.Handlers.AnswerHandlers.Commands;

namespace OnlineEducation.API.Controllers
{
    public class AnswerController : BaseController
    {
        [RequestSizeLimit(250000000)]
        [HttpPost("{questionId}/video")]
        public async Task<ActionResult<Unit>> AddAnswerVideoForQuestion(Guid questionId, [FromForm] VideoFile videoFile)
        {
            return await Mediator.Send(new CreateAnswerVideoForAnswer.Command
                {QuestionId = questionId, File = videoFile.File});
        }

        [HttpPost("{questionId}/image")]
        public async Task<ActionResult<Unit>> AddAnswerImageForQuestion(Guid questionId, [FromForm] ImageFile imageFile)
        {
            return await Mediator.Send(new CreateAnswerImageForAnswer.Command
                {QuestionId = questionId, File = imageFile.File});
        }
    }
}
