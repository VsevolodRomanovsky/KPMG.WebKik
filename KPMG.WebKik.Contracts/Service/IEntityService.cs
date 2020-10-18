using System.Collections.Generic;
using System.Threading.Tasks;

namespace KPMG.WebKik.Contracts.Service
{
    public interface IEntityService<TEntity, in TKey> : IBaseService
    {
        Task<TEntity> GetById(TKey id);

        Task<TEntity> Create(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TKey id);

        Task<IList<TEntity>> GetAll();
    }

}
