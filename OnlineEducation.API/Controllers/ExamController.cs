using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.API.Helper;
using OnlineEducation.Business.Handlers.ExamQuestionHandlers.Commands;
using OnlineEducation.Business.Handlers.ExamQuestionHandlers.Queries;
using OnlineEducation.Business.Handlers.VideoAnswerForExamQuestionHandlers.Commands;
using OnlineEducation.Entities.Dtos;

namespace OnlineEducation.API.Controllers
{
    public class ExamController : BaseController
    {
        [HttpGet("{chapterId}/exam")]
        public async Task<IReadOnlyList<ExamQuestionDto>> GetExam(Guid chapterId, [FromQuery] int count = 20)
        {
            return await Mediator.Send(new GetExam.Query {Count = count, ChapterId = chapterId});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateExamQuestion.Command command)
        {
            return await Mediator.Send(command);
        }

        [RequestSizeLimit(250000000)]
        [HttpPost("{id}/createAnswerVide")]
        public async Task<ActionResult<Unit>> CreateVideoAnswer(Guid id, [FromForm] VideoFile videoFile)
        {
            return await Mediator.Send(new CreateVideoAnswerForExamQuestion.Command
                {ExamQuestionId = id, File = videoFile.File});
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, EditExamQuestion.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteExamQuestion.Command {Id = id});
        }

        [HttpDelete("{id}/deleteAnswerVideo")]
        public async Task<ActionResult<Unit>> DeleteVideoAnswer(Guid id)
        {
            return await Mediator.Send(new DeleteVideoAnswerForExamQuestion.Command {ExamQuestionId = id});
        }
    }
}
