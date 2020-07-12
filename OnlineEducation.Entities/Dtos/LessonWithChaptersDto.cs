using System;
using System.Collections.Generic;

namespace OnlineEducation.Entities.Dtos
{
    public class LessonWithChaptersDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ChapterDto> Chapters { get; set; }
    }
}
