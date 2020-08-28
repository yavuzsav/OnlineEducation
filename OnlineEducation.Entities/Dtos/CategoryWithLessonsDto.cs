using System;
using System.Collections.Generic;

namespace OnlineEducation.Entities.Dtos
{
    public class CategoryWithLessonsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<LessonDto> Lessons { get; set; }
    }
}
