using System;
using System.Collections.Generic;

namespace OnlineEducation.Entities.Dtos
{
    public class ChapterWithChapterVideosDto
    {
        public Guid Id { get; set; }
        public string LessonName { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }

        public ICollection<ChapterVideoDto> ChapterVideos { get; set; }
    }
}
