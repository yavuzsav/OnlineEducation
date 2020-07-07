using System;
using System.Collections;
using System.Threading.Tasks;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Abstract;

namespace OnlineEducation.DataAccess.Concrete.EntityFramework
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly OnlineEducationContext _context;
        private Hashtable _repositories;

        public EfUnitOfWork(OnlineEducationContext context)
        {
            _context = context;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity, new()
        {
            _repositories ??= new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(EfGenericRepository<>);
                var repositoryInstance =
                    Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>) _repositories[type];
        }

        public async Task<int> CompleteAsync()
        {
            var returnValue = 0;
            await using var dbContextTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                returnValue = await _context.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await dbContextTransaction.RollbackAsync();
            }

            return returnValue;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
