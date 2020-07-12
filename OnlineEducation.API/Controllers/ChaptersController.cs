using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.Business.Handlers.ChapterHandlers.Commands;
using OnlineEducation.Business.Handlers.ChapterHandlers.Queries;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.Entities.Dtos;

namespace OnlineEducation.API.Controllers
{
    public class ChaptersController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Pagination<ChapterDto>>> GetChapters(
            [FromQuery] PaginationParams paginationParams)
        {
            return await Mediator.Send(new GetChapters.Query {PaginationParams = paginationParams});
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChapterWithChapterVideosDto>> GetChapter(Guid id)
        {
            return await Mediator.Send(new GetChapterDetails.Query {ChapterId = id});
        }

        [HttpPost]
        public async Task<Unit> Create(CreateChapter.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<Unit> Edit(Guid id, EditChapter.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<Unit> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteChapter.Command {Id = id});
        }
    }
}
