using FluentValidation;

namespace OnlineEducation.Business.Extensions
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .NotEmpty()
                .MinimumLength(8).WithMessage("Password must be at least 6 characters");

            return options;
        }
    }
}
