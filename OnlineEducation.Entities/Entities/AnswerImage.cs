using System;

namespace OnlineEducation.Entities.Entities
{
    public class AnswerImage
    {
        public Guid Id { get; set; }
        public string PublicId { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid AnswerId { get; set; }
        public Answer Answer { get; set; }
    }
}