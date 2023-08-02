using Company.Application.Interfaces;
using Company.Persistence.DB;
using Microsoft.EntityFrameworkCore;

namespace Company.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DataContext _dbContext;
        public GenericRepository(DataContext context)
        {
            _dbContext = context;
        }

        public async Task<T> AddEntity(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<List<T>?> DeleteEntity(int id)
        {
            var entities = _dbContext.Set<T>();
            var entity = await entities.FindAsync(id);
            if (entity is null)
                return null;

            entities.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return await entities.ToListAsync();
        }

        public async Task<List<T>> DeleteRange<Y>(Y[] values) where Y : class
        {
            var entities = _dbContext.Set<Y>();
            entities.RemoveRange(values);
            await _dbContext.SaveChangesAsync();
            return await _dbContext.Set<T>().ToListAsync();
        }


        public async Task<List<T>> GetAllEntity()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetEntity(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            return entity;
        }

        public virtual async Task<T> UpdateEntity(int id, T req)
        {
            var entity = await GetEntity(id);
            if (entity == null)
                throw new InvalidOperationException("ჩანაწერი ვერ მოიძებნა");
            var entryObject = _dbContext.Entry(entity);
            entryObject.CurrentValues.SetValues(req);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
