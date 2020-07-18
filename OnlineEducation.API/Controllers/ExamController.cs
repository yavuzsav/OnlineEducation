using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.Business.Handlers.ExamQuestionHandlers.Commands;
using OnlineEducation.Business.Handlers.ExamQuestionHandlers.Queries;
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
    }
}
