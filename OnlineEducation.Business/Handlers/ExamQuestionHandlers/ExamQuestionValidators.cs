using FluentValidation;
using OnlineEducation.Business.Handlers.ExamQuestionHandlers.Commands;

namespace OnlineEducation.Business.Handlers.ExamQuestionHandlers
{
    public class CreateExamQuestionValidator : AbstractValidator<CreateExamQuestion.Command>
    {
        public CreateExamQuestionValidator()
        {
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.Option1).NotEmpty();
            RuleFor(x => x.Option2).NotEmpty();
            RuleFor(x => x.Option3).NotEmpty();
            RuleFor(x => x.Option4).NotEmpty();
            RuleFor(x => x.CorrectAnswer).NotNull();
        }
    }

    public class EditExamQuestionValidator : AbstractValidator<EditExamQuestion.Command>
    {
        public EditExamQuestionValidator()
        {
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.Option1).NotEmpty();
            RuleFor(x => x.Option2).NotEmpty();
            RuleFor(x => x.Option3).NotEmpty();
            RuleFor(x => x.Option4).NotEmpty();
            RuleFor(x => x.CorrectAnswer).NotNull().IsInEnum();
        }
    }
}
