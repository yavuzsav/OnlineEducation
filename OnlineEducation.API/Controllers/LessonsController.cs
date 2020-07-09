using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.Business.Handlers.Lesson.Commands;
using OnlineEducation.Business.Handlers.Lesson.Queries;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.API.Controllers
{
    public class LessonsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Pagination<Lesson>>> GetAll([FromQuery] PaginationParams paginationParams)
        {
            return await Mediator.Send(new LessonList.Query {PaginationParams = paginationParams});
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
