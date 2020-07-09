using System;
using OnlineEducation.DataAccess.Specifications;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Specifications.CategorySpecifications
{
    public class CategoryWithLessonsSpecification : BaseSpecification<Category>
    {
        public CategoryWithLessonsSpecification(Guid categoryId) : base(x => x.Id == categoryId)
        {
            AddInclude(x => x.Lessons);
        }
    }
}
