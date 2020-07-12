using FluentValidation;
using OnlineEducation.Business.Handlers.ChapterHandlers.Commands;

namespace OnlineEducation.Business.Handlers.ChapterHandlers
{
    public class CreateChapterValidator : AbstractValidator<CreateChapter.Command>
    {
        public CreateChapterValidator()
        {
            RuleFor(x => x.LessonId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
        }
    }

    public class EditChapterValidator : AbstractValidator<EditChapter.Command>
    {
        public EditChapterValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.LessonId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Content).NotEmpty();
        }
    }
}
