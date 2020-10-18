using System.Linq;
using System.Web.Http;
using AutoMapper;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Models;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace KPMG.WebKik.Web.Controllers
{
    public abstract class EntityController<TEntity, TViewModel, TKey> : BaseController
        where TEntity : IEntity<TKey>
        where TViewModel : IEntity<TKey>
    {
        protected EntityController(IEntityService<TEntity, TKey> service) : base(service)
        {
        }

        [HttpGet, Route("{id:int}")]
        public virtual async Task<TViewModel> GetById(TKey id)
        {
            var result = await ((IEntityService<TEntity, TKey>) Service).GetById(id);
            return Mapper.Map<TViewModel>(result);
        }

        [HttpGet, Route("")]
        public virtual async Task<IQueryable<TViewModel>> GetAll()
        {
            var result = await ((IEntityService<TEntity, TKey>) Service).GetAll();
            return result.AsQueryable().ProjectTo<TViewModel>();
        }

        [HttpPost, Route("")]
        public virtual async Task<TViewModel> Create([FromBody]TViewModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            entity = await ((IEntityService<TEntity, TKey>) Service).Create(entity);
            return Mapper.Map<TViewModel>(entity);
        }

        [HttpPut, Route("")]
        public virtual async Task Update([FromBody]TViewModel model)
        {
            var entity = Mapper.Map<TEntity>(model);
            await ((IEntityService<TEntity, TKey>) Service).Update(entity);
        }

        [HttpDelete, Route("{id:int}")]
        public virtual async Task Delete(TKey id)
        {
            await ((IEntityService<TEntity, TKey>) Service).Delete(id);
        }
    }
}