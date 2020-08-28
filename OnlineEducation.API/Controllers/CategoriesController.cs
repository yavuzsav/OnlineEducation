using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.Business.Handlers.CategoryHandlers.Commands;
using OnlineEducation.Business.Handlers.CategoryHandlers.Queries;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.Entities.Dtos;

namespace OnlineEducation.API.Controllers
{
    public class CategoriesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Pagination<CategoryDto>>> GetCategories(
            [FromQuery] PaginationParams paginationParams)
        {
            return await Mediator.Send(new CategoryList.Query { PaginationParams = paginationParams });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryWithLessonsDto>> GetCategory(Guid id)
        {
            return await Mediator.Send(new GetCategoryWithLessons.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateCategory.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, EditCategory.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteCategory.Command { Id = id });
        }
    }
}
