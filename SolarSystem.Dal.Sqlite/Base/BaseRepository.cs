using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SolarSystem.Dal.Abstraction.Base;

namespace SolarSystem.Dal.Sqlite.Base
{
    public abstract class BaseRepository<TEntity,TKey>:ICrud<TEntity,TKey> 
        where TEntity:class, IEntity<TKey>
    {
        protected readonly SolarDbContext _dbContext;

        protected BaseRepository(SolarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            var result = _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var result = _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TEntity> Delete(TEntity entity)
        {
            var result = _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TEntity> Delete(TKey key)
        {
            var entity = await Get(key);
            var result =await Delete(entity);
            return result;
        }

        public virtual Task<TEntity> Get(TKey key)
        {
            return _dbContext.Set<TEntity>().FirstOrDefaultAsync(e => Equals(e.Id, key));
        }

        public Task<List<TEntity>> Get()
        {
            return _dbContext.Set<TEntity>().ToListAsync();
        }
    }
}