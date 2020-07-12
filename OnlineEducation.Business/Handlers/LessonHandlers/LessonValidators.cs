using FluentValidation;
using OnlineEducation.Business.Handlers.LessonHandlers.Commands;

namespace OnlineEducation.Business.Handlers.LessonHandlers
{
    public class CreateLessonValidator : AbstractValidator<CreateLesson.Command>
    {
        public CreateLessonValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    public class EditLessonValidator : AbstractValidator<EditLesson.Command>
    {
        public EditLessonValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
