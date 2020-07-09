using FluentValidation;
using OnlineEducation.Business.Extensions;
using OnlineEducation.Business.Handlers.User.Commands;

namespace OnlineEducation.Business.Handlers.User
{
    public class LoginValidator : AbstractValidator<Login.Command>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).Password();
        }
    }

    public class RegisterValidator : AbstractValidator<Register.Command>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).Password();
        }
    }
}
