using System;
using System.Collections.Generic;
using OnlineEducation.Entities.Abstract;

namespace OnlineEducation.Entities.Entities
{
    public class Answer : IEntity
    {
        public Guid Id { get; set; }

        public AnswerVideo AnswerVideo { get; set; }
        public ICollection<AnswerImage> AnswerImages { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
