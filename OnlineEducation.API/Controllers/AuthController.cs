using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEducation.Business.Handlers.User.Commands;
using OnlineEducation.Entities.Dtos;

namespace OnlineEducation.API.Controllers
{
    public class AuthController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(Register.Command command)
        {
            return await Mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(Login.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}
