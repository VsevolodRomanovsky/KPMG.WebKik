using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models;

namespace KPMG.WebKik.Data
{
    public class EntityRepository<TEntity, TKey> : IEntityRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : struct
    {
        private IQueryable<TEntity> query;

        public DbContext DbContext;

        protected IQueryable<TEntity> Query
        {
            get { return query ?? (query = DbContext.Set<TEntity>().AsQueryable()); }
            set { query = value; }
        }

        public EntityRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void ReloadDataContext()
        {
            // хак-хак! Без этого не работает загрузка словарей через WebApi. Нужно сделать по-нормальному
            DbContext = new WebKikDataContext();
        }

        public IEntityRepository<TEntity, TKey> ClearQuery()
        {
            query = null;
            return this;
        }

        public IEntityRepository<TEntity, TKey> Include(params Expression<Func<TEntity, object>>[] properties)
        {
            Query = properties.Aggregate(Query, (current, property) => current.Include(property));
            return this;
        }

        public IEntityRepository<TEntity, TKey> Where(Expression<Func<TEntity, bool>> filter)
        {
            Query = Query.Where(filter);
            return this;
        }

        public IEntityRepository<TEntity, TKey> Order<TField>(Expression<Func<TEntity, TField>> order, bool isAscending)
        {
            return isAscending ? OrderBy(order) : OrderByDesc(order);
        }

        public IEntityRepository<TEntity, TKey> OrderBy<TField>(Expression<Func<TEntity, TField>> order)
        {
            Query = Query.OrderBy(order);
            return this;
        }

        public IEntityRepository<TEntity, TKey> OrderByDesc<TField>(Expression<Func<TEntity, TField>> order)
        {
            Query = Query.OrderByDescending(order);
            return this;
        }

        public IEntityRepository<TEntity, TKey> Skip(int count)
        {
            Query = Query.Skip(count);
            return this;
        }

        public IEntityRepository<TEntity, TKey> Take(int count)
        {
            Query = Query.Take(count);
            return this;
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> FirstOrDefaultAsync()
        {
            return await Query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> FirstAsync()
        {
            var result = await Query.FirstAsync();
            return result;
        }

        public async Task<TEntity> SingleOrDefaultAsync()
        {
            return await Query.SingleOrDefaultAsync();
        }

        public async Task<TEntity> SingleAsync()
        {
            return await Query.SingleAsync();
        }

        public async Task<IList<TEntity>> ToListAsync()
        {
            return await Query.ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await Query.CountAsync();
        }

        public void Add(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            var entry = DbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                DbContext.Set<TEntity>().Attach(entity);
                entry = DbContext.Entry(entity);
            }
            entry.State = EntityState.Modified;
        }

        public async Task DeleteById(TKey id)
        {
            var entity = await GetByIdAsync(id);
            Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
