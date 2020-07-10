using System;
using OnlineEducation.Entities.Abstract;

namespace OnlineEducation.Entities.Entities
{
    public class ChapterVideo : IEntity
    {
        public Guid Id { get; set; }
        public string PublicId { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid ChapterId { get; set; }
        public Chapter Chapter { get; set; }
    }
}
