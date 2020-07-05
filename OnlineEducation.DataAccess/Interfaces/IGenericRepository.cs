using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineEducation.Entities.Abstract;

namespace OnlineEducation.DataAccess.Interfaces
{
    public interface IGenericRepository<T> where T : class, IEntity, new()
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
