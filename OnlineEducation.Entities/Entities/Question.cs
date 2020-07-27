using System;
using OnlineEducation.Entities.Abstract;
using OnlineEducation.Entities.Identity;

namespace OnlineEducation.Entities.Entities
{
    public class Question : IEntity
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsAnswerVideo { get; set; }

        public string OwnerId { get; set; }
        public AppUser Owner { get; set; }

        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public QuestionImage QuestionImage { get; set; }
        public Answer Answer { get; set; }
    }
}
