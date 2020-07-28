using System;

namespace OnlineEducation.Entities.Dtos
{
    public class AnswerImageDto
    {
        public Guid Id { get; set; }
        public string PublicId { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}