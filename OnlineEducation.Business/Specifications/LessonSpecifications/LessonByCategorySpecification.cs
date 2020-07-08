using System;
using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.DataAccess.Specifications;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Specifications.LessonSpecifications
{
    public class LessonByCategorySpecification : BaseSpecification<Lesson>
    {
        public LessonByCategorySpecification(Guid categoryId) : base(x => x.CategoryId == categoryId)
        {
        }

        public LessonByCategorySpecification(Guid categoryId, PaginationParams paginationParams) : base(x =>
            x.CategoryId == categoryId)
        {
            AddInclude(x => x.Category);
            ApplyPaging(paginationParams.PageSize * (paginationParams.PageIndex - 1), paginationParams.PageSize);

            if (!string.IsNullOrEmpty(paginationParams.Sort))
            {
                switch (paginationParams.Sort)
                {
                    case "Asc":
                        AddOrderBy(c => c.Name);
                        break;
                    case "Desc":
                        AddOrderByDescending(c => c.Name);
                        break;
                    default:
                        AddOrderBy(c => c.Name);
                        break;
                }
            }
        }
    }
}
