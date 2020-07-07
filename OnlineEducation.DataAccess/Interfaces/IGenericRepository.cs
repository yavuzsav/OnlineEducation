using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineEducation.DataAccess.Specifications;
using OnlineEducation.Entities.Abstract;

namespace OnlineEducation.DataAccess.Interfaces
{
    public interface IGenericRepository<T> where T : class, IEntity, new()
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAllAsync();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> GetEntityWithSpecificationAsync(ISpecification<T> specification);
        Task<IReadOnlyList<T>> ListWithSpecificationAsync(ISpecification<T> specification);
        Task<int> CountAsync(ISpecification<T> specification);
    }
}
