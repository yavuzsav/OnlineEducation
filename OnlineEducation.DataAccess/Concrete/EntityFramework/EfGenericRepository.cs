using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineEducation.DataAccess.Interfaces;
using OnlineEducation.Entities.Abstract;

namespace OnlineEducation.DataAccess.Concrete.EntityFramework
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : class, IEntity, new()
    {
        private readonly OnlineEducationContext _context;

        public EfGenericRepository(OnlineEducationContext context)
        {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
