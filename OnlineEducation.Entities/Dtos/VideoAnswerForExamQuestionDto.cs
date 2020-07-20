using System;

namespace OnlineEducation.Entities.Dtos
{
    public class VideoAnswerForExamQuestionDto
    {
        public Guid Id { get; set; }
        public string PublicId { get; set; }
        public string Url { get; set; }
    }
}