using Microsoft.EntityFrameworkCore;
using ProductManagerAPI.Repositories.Interfaces;
using ProductManagerAPI.Services.Interfaces;
using System.Linq.Expressions;

namespace ProductManagerAPI.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
            
    {
        protected readonly IUnitOfWork _unitOfWork;

        public Service(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _unitOfWork.GetRepository<IBaseRepository<TEntity>>().GetAllAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _unitOfWork.GetRepository<IBaseRepository<TEntity>>().GetAsync(predicate);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var createdEntity = _unitOfWork.GetRepository<IBaseRepository<TEntity>>().Create(entity);
            await _unitOfWork.Commit();
            return createdEntity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            var deletedEntity = _unitOfWork.GetRepository<IBaseRepository<TEntity>>().Delete(entity);
            await _unitOfWork.Commit();
            return deletedEntity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var updatedEntity = _unitOfWork.GetRepository<IBaseRepository<TEntity>>().Update(entity);
            await _unitOfWork.Commit();
            return updatedEntity;
        }
    }
}
