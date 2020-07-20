using System;
using System.Linq;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.DataAccess.Specifications;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Specifications.ChapterSpecifications
{
    public class ChapterWithQuestionsSpecification : BaseSpecification<Chapter>
    {
        public ChapterWithQuestionsSpecification(Guid chapterId) : base(x => x.Id == chapterId)
        {
            AddInclude(x => x.ExamQuestions);
        }

        public ChapterWithQuestionsSpecification(Guid chapterId, PaginationParams paginationParams) : base(x =>
            x.Id == chapterId)
        {
            AddInclude(x => x.ExamQuestions);
            AddNestedInclude("ExamQuestions.VideoAnswerForExamQuestion");
            ApplyPaging(paginationParams.PageSize * (paginationParams.PageIndex - 1), paginationParams.PageSize);

            if (!string.IsNullOrEmpty(paginationParams.Sort))
            {
                switch (paginationParams.Sort)
                {
                    case "Asc":
                        AddOrderBy(c => c.Name);
                        break;
                    case "Desc":
                        AddOrderByDescending(c => c.Name);
                        break;
                    default:
                        AddOrderBy(c => c.Name);
                        break;
                }
            }
        }
    }
}
