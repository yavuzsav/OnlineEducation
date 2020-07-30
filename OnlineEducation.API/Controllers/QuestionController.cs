using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.API.Helper;
using OnlineEducation.Business.Handlers.QuestionHandlers.Commands;
using OnlineEducation.Business.Handlers.QuestionHandlers.Queries;
using OnlineEducation.Business.Handlers.QuestionImageHandlers.Commands;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.Entities.Dtos;

namespace OnlineEducation.API.Controllers
{
    public class QuestionController : BaseController
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<QuestionDto>>> GetUserQuestions(
            [FromQuery] PaginationParams paginationParams)
        {
            return await Mediator.Send(new GetUserQuestions.Query {PaginationParams = paginationParams});
        }

        [HttpGet("unanswered")]
        public async Task<ActionResult<Pagination<QuestionDto>>> GetUserUnansweredQuestions(
            [FromQuery] PaginationParams paginationParams)
        {
            return await Mediator.Send(new GetUserUnansweredQuestions.Query {PaginationParams = paginationParams});
        }

        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Teacher")]
        [HttpGet("AllUnanswered")]
        public async Task<ActionResult<Pagination<QuestionDto>>> GetAllUserUnansweredQuestions(
            [FromQuery] PaginationParams paginationParams)
        {
            return await Mediator.Send(new GetAllUnansweredQuestions.Query {PaginationParams = paginationParams});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateQuestion.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("{questionId}/addImage")]
        public async Task<ActionResult<Unit>> AddImage(Guid questionId, [FromForm] ImageFile imageFile)
        {
            return await Mediator.Send(new CreateQuestionImage.Command
                {QuestionId = questionId, File = imageFile.File});
        }
    }
}
