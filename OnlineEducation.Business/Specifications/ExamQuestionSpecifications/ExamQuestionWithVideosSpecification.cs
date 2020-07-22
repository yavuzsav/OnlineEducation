using System;
using OnlineEducation.DataAccess.Specifications;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Specifications.ExamQuestionSpecifications
{
    public class ExamQuestionWithVideosSpecification : BaseSpecification<ExamQuestion>
    {
        public ExamQuestionWithVideosSpecification(Guid examQuestionId) : base(x => x.Id == examQuestionId)
        {
            AddInclude(x => x.VideoAnswerForExamQuestion);
        }
    }
}
