using System;
using System.Threading.Tasks;
using OnlineEducation.Entities.Abstract;

namespace OnlineEducation.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity, new();
        Task<int> Complete();
    }
}
