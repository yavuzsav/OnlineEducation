using System;

namespace OnlineEducation.Entities.Dtos
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAnswerVideo { get; set; }
        public string LessonName { get; set; }
        public string CategoryName { get; set; }
        public QuestionImageDto QuestionImageDto { get; set; }
    }
}
