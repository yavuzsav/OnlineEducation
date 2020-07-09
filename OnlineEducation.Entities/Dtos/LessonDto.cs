using System;

namespace OnlineEducation.Entities.Dtos
{
    public class LessonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
