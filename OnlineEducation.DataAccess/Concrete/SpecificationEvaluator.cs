﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineEducation.DataAccess.Specifications;
using OnlineEducation.Entities.Abstract;

namespace OnlineEducation.DataAccess.Concrete
{
    public class SpecificationEvaluator<TEntity> where TEntity : class, IEntity, new()
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            if (specification.NestedIncludes.Count > 0)
            {
                query = specification.NestedIncludes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
