using System;
using OnlineEducation.Entities.Abstract;
using OnlineEducation.Entities.Enums;

namespace OnlineEducation.Entities.Entities
{
    public class ExamQuestion : IEntity
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public CorrectAnswer CorrectAnswer { get; set; }

        public Guid ChapterId { get; set; }
        public Chapter Chapter { get; set; }
    }
}
