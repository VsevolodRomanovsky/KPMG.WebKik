using System.Collections.Generic;
using System.Threading.Tasks;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Models;
using KPMG.WebKik.Contracts.Repository;

namespace KPMG.WebKik.Services
{
    public class EntityService<TEntity, TKey> : BaseService, IEntityService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : struct
    {
        protected readonly IEntityRepository<TEntity, TKey> repository;
        public EntityService(IEntityRepository<TEntity, TKey> repository)
        {
            this.repository = repository;
        }

        public void ReloadDataContext()
        {
            // хак-хак! Без этого не работает загрузка словарей через WebApi. Нужно сделать по-нормальному
            ((dynamic)repository).ReloadDataContext();
        }

        public virtual async Task<TEntity> GetById(TKey id)
        {
            var result = await repository.GetByIdAsync(id);
            return result;
        }

        public virtual async Task<IList<TEntity>> GetAll()
        {
            return await repository.ToListAsync();
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            repository.Add(entity);
            await repository.SaveChangesAsync();
            return entity;
        }

        public virtual async Task Update(TEntity entity)
        {
            repository.Update(entity);
            await repository.SaveChangesAsync();
        }

        public virtual async Task Delete(TKey id)
        {
            await repository.DeleteById(id);
            await repository.SaveChangesAsync();
        }

        public override void Dispose()
        {
            repository.Dispose();
        }
    }
}
