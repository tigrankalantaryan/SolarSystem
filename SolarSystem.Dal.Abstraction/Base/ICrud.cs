using System.Collections.Generic;
using System.Threading.Tasks;

namespace SolarSystem.Dal.Abstraction.Base
{
    public interface ICrud<TEntity, in TKey>
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(TEntity entity);
        Task<TEntity> Delete(TKey key);
        Task<TEntity> Get(TKey key);
        Task<List<TEntity>> Get();
    }
}