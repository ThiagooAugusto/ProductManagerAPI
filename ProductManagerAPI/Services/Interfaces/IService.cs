using System.Linq.Expressions;

namespace ProductManagerAPI.Services.Interfaces
{
    public interface IService<TEntity> where TEntity : class
    {
        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<TEntity> CreateAsync(TEntity entity);
        public Task<TEntity> UpdateAsync(TEntity entity);
        public Task<TEntity> DeleteAsync(TEntity entity);
    }
}
