using System;

namespace OnlineEducation.Entities.Dtos
{
    public class QuestionImageDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
    }
}