using System;

namespace OnlineEducation.Entities.Entities
{
    public class QuestionImage
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }

        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
