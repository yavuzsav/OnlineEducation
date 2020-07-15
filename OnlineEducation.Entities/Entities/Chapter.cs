using System;
using System.Collections.Generic;
using OnlineEducation.Entities.Abstract;

namespace OnlineEducation.Entities.Entities
{
    public class Chapter : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }

        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public ICollection<ChapterVideo> ChapterVideos { get; set; }
        public ICollection<ExamQuestion> ExamQuestions { get; set; }
    }
}
