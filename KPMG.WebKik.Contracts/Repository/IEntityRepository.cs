using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using KPMG.WebKik.Models;

namespace KPMG.WebKik.Contracts.Repository
{
    public interface IEntityRepository<TEntity, in TKey> : IDisposable
        where TEntity : class, IEntity<TKey>
        where TKey: struct
    {
        void Add(TEntity entity);
        Task<int> CountAsync();
        Task DeleteById(TKey id);
        void Delete(TEntity entity);
        Task<TEntity> FirstAsync();
        Task<TEntity> FirstOrDefaultAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        IEntityRepository<TEntity, TKey> Include(params Expression<Func<TEntity, object>>[] properties);
        IEntityRepository<TEntity, TKey> Order<TField>(Expression<Func<TEntity, TField>> order, bool isAscending);
        IEntityRepository<TEntity, TKey> OrderBy<TField>(Expression<Func<TEntity, TField>> order);
        IEntityRepository<TEntity, TKey> OrderByDesc<TField>(Expression<Func<TEntity, TField>> order);
        Task<TEntity> SingleAsync();
        Task<TEntity> SingleOrDefaultAsync();
        IEntityRepository<TEntity, TKey> Skip(int count);
        IEntityRepository<TEntity, TKey> Take(int count);
        Task<IList<TEntity>> ToListAsync();
        void Update(TEntity entity);
        IEntityRepository<TEntity, TKey> Where(Expression<Func<TEntity, bool>> filter);
        IEntityRepository<TEntity, TKey> ClearQuery();
        Task SaveChangesAsync();
    }
}
