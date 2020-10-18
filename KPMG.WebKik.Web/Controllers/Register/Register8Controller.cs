using System.Web.Http;
using KPMG.WebKik.Contracts.Service.Registers;
using AutoMapper;
using KPMG.WebKik.Models.Registers;
using System.Threading.Tasks;
using KPMG.WebKik.Services.Registers;

namespace KPMG.WebKik.Web.Controllers.Register
{
    [RoutePrefix("api/register8")]
    public class Register8Controller : ApiController
	{
        public Register8Controller(IRegister8Service service)
        {
        }

		[HttpGet, Route("{id:int}")]
		public virtual Register8Dto GetById(int id)
		{
			Register8Service service = new Register8Service();
			var result = service.GetRegister8(id);
			return Mapper.Map<Register8Dto>(result);
		}

		[HttpPost, Route("")]
		public virtual void Create([FromBody]Register8Dto register)
		{
			Register8Service service = new Register8Service();
			var entity = Mapper.Map<Register8>(register);
			var result = service.Create(entity);
			return;
		}

		[HttpPost, Route("edit")]
		public virtual void Edit([FromBody]Register8Dto register)
		{
			Register8Service service = new Register8Service();
			var entity = Mapper.Map<Register8>(register);
			var result = service.Edit(entity);
			return;
		}

		[HttpPost, Route("calculate")]
        public Register8ViewModel Calculate([FromBody]Register8ViewModel model)
        {
            var entity = Mapper.Map<Register8>(model);
            return Mapper.Map<Register8ViewModel>(entity);
        }
    }
}
