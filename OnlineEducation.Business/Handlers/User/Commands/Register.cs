using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineEducation.Core.ErrorHelpers;
using OnlineEducation.Core.Security;
using OnlineEducation.Entities.Dtos;
using OnlineEducation.Entities.Identity;

namespace OnlineEducation.Business.Handlers.User.Commands
{
    public class Register
    {
        public class Command : IRequest<UserDto>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class Handler : IRequestHandler<Command, UserDto>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
            {
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _userManager.Users.AnyAsync(x => x.Email == request.Email,
                    cancellationToken: cancellationToken))
                    throw new RestException(HttpStatusCode.BadRequest, new {Email = "Email already exists"});

                var user = new AppUser
                {
                    Email = request.Email,
                    UserName = request.Email,
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return new UserDto
                    {
                        Email = user.Email,
                        Token = await _jwtGenerator.CreateTokenAsync(user)
                    };
                }

                throw new Exception("Problem creating user");
            }
        }
    }
}
