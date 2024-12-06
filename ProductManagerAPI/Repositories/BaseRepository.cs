using Microsoft.EntityFrameworkCore;
using ProductManagerAPI.Context;
using ProductManagerAPI.Repositories.Interfaces;
using System.Linq.Expressions;

namespace ProductManagerAPI.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T:class
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }

       
        public T Update(T entity)
        {
           _context.Set<T>().Update(entity);
            return entity;
        }
    }
}
