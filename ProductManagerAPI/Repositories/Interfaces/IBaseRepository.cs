using System.Linq.Expressions;

namespace ProductManagerAPI.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        public Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        public Task<IEnumerable<T>> GetAllAsync();
        public T Create(T entity);
        public T Update(T entity);
        public T Delete(T entity);
      
    }
}
