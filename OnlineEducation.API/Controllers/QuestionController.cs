using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.Business.Handlers.QuestionHandlers.Commands;
using OnlineEducation.Business.Handlers.QuestionHandlers.Queries;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.Entities.Dtos;

namespace OnlineEducation.API.Controllers
{
    public class QuestionController : BaseController
    {
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<QuestionDto>>> GetQuestionsByUser([FromQuery]PaginationParams paginationParams)
        {
            return await Mediator.Send(new GetQuestionsByUser.Query {PaginationParams = paginationParams});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateQuestion.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
