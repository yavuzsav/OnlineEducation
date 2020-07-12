using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.Business.Handlers.LessonHandlers.Commands;
using OnlineEducation.Business.Handlers.LessonHandlers.Queries;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.Entities.Dtos;

namespace OnlineEducation.API.Controllers
{
    public class LessonsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Pagination<LessonWithCategoryNameDto>>> GetAll(
            [FromQuery] PaginationParams paginationParams)
        {
            return await Mediator.Send(new LessonList.Query {PaginationParams = paginationParams});
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonWithChaptersDto>> GetDetails(Guid id)
        {
            return await Mediator.Send(new LessonDetails.Query {Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateLesson.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, EditLesson.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteLesson.Command {Id = id});
        }
    }
}
