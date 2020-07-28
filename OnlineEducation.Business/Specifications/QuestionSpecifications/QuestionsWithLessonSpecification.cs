using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.DataAccess.Specifications;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Specifications.QuestionSpecifications
{
    public class QuestionsWithLessonSpecification : BaseSpecification<Question>
    {
        public QuestionsWithLessonSpecification(string userId) : base(x => x.OwnerId == userId)
        {
        }

        public QuestionsWithLessonSpecification(string userId, PaginationParams paginationParams) : base(x =>
            x.OwnerId == userId)
        {
            AddInclude(x => x.Lesson);
            AddInclude(x => x.QuestionImage);
            AddInclude(x => x.Answer);
            AddNestedInclude("Answer.AnswerVideo");
            AddNestedInclude("Answer.AnswerImages");
            AddNestedInclude("Lesson.Category");
            ApplyPaging(paginationParams.PageSize * (paginationParams.PageIndex - 1), paginationParams.PageSize);

            if (!string.IsNullOrEmpty(paginationParams.Sort))
            {
                switch (paginationParams.Sort)
                {
                    case "Asc":
                        AddOrderBy(c => c.CreatedAt);
                        break;
                    case "Desc":
                        AddOrderByDescending(c => c.CreatedAt);
                        break;
                    default:
                        AddOrderBy(c => c.CreatedAt);
                        break;
                }
            }
        }
    }
}
