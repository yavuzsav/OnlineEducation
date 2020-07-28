using System;
using OnlineEducation.DataAccess.Specifications;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Specifications.QuestionSpecifications
{
    public class QuestionWithAnswerSpecification : BaseSpecification<Question>
    {
        public QuestionWithAnswerSpecification(Guid questionId) : base(x => x.Id == questionId)
        {
            AddInclude(x => x.Answer);
            AddNestedInclude("Answer.AnswerVideo");
            AddNestedInclude("Answer.AnswerImages");
        }
    }
}
