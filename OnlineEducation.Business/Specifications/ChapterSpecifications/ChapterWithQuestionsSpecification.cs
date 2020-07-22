using System;
using OnlineEducation.DataAccess.Specifications;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Specifications.ChapterSpecifications
{
    public class ChapterWithQuestionsSpecification : BaseSpecification<Chapter>
    {
        public ChapterWithQuestionsSpecification(Guid chapterId) : base(x =>
            x.Id == chapterId)
        {
            AddInclude(x => x.ExamQuestions);
            AddNestedInclude("ExamQuestions.VideoAnswerForExamQuestion");
        }
    }
}
