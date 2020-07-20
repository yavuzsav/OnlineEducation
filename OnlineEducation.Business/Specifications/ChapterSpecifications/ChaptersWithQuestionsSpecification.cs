using System;
using System.Linq;
using OnlineEducation.DataAccess.Specifications;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Specifications.ChapterSpecifications
{
    public class ChaptersWithQuestionsSpecification : BaseSpecification<Chapter>
    {
        public ChaptersWithQuestionsSpecification(Guid chapterId) : base(x => x.Id == chapterId)
        {
            AddInclude(x => x.ExamQuestions);
            AddNestedInclude("ExamQuestions.VideoAnswerForExamQuestion");
        }
    }
}
