using OnlineEducation.Core.PaginationHelper;
using OnlineEducation.DataAccess.Specifications;
using OnlineEducation.Entities.Entities;

namespace OnlineEducation.Business.Specifications.CategorySpecifications
{
    public class CategorySpecification : BaseSpecification<Category>
    {
        public CategorySpecification()
        {
        }

        public CategorySpecification(PaginationParams paginationParams)
        {
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
