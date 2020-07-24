using FluentValidation;
using OnlineEducation.Business.Handlers.QuestionHandlers.Commands;

namespace OnlineEducation.Business.Handlers.QuestionHandlers
{
    public class CreateQuestionValidator : AbstractValidator<CreateQuestion.Command>
    {
        public CreateQuestionValidator()
        {
            RuleFor(x => x.LessonId).NotEmpty();
        }
    }
}
