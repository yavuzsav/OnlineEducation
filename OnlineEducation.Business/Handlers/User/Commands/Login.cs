using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.Core.Security;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Identity;

namespace OnlineEducation.Business.Handlers.User.Commands
{
    public class Login
    {
        public class Command : IRequest<UserDto>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Command, UserDto>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
                IJwtGenerator jwtGenerator)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                    throw new RestException(HttpStatusCode.Unauthorized);

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    return new UserDto
                    {
                        Email = user.Email,
                        Token = await _jwtGenerator.CreateTokenAsync(user)
                    };
                }

                throw new RestException(HttpStatusCode.Unauthorized);
            }
        }
    }
}
