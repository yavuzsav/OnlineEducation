using System;
using OnlineEducation.DataAccess.Specifications;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Specifications.LessonSpecifications
{
    public class LessonWithChaptersSpecification : BaseSpecification<Lesson>
    {
        public LessonWithChaptersSpecification(Guid lessonId) : base(x => x.Id == lessonId)
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Chapters);
        }
    }
}
