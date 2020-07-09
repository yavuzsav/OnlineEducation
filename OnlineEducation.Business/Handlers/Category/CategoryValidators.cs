using FluentValidation;
using OnlineEducation.Business.Handlers.Category.Commands;

namespace OnlineEducation.Business.Handlers.Category
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategory.Command>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    public class EditCategoryValidator : AbstractValidator<EditCategory.Command>
    {
        public EditCategoryValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
