using System;

namespace OnlineEducation.Entities.Dtos
{
    public class LessonWithCategoryNameDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
