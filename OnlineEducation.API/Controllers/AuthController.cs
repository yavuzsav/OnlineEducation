using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.Business.Handlers.User.Commands;
using OnlineEducation.Entities.Dtos;

namespace OnlineEducation.API.Controllers
{
    public class AuthController : BaseController
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(Register.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
