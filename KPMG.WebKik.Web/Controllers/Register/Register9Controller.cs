using System.Web.Http;
using KPMG.WebKik.Contracts.Service.Registers;
using AutoMapper;
using KPMG.WebKik.Models.Registers;
using System.Threading.Tasks;
using KPMG.WebKik.Services.Registers;

namespace KPMG.WebKik.Web.Controllers.Register
{
    [RoutePrefix("api/register9")]
    public class Register9Controller : ApiController
	{
        public Register9Controller(IRegister9Service service)
        {
        }

		[HttpGet, Route("{id:int}")]
		public virtual Register9ViewModel GetById(int id)
		{
			Register9Service service = new Register9Service();
			var result = service.GetRegister9(id);
			return Mapper.Map<Register9ViewModel>(result);
		}

		[HttpPost, Route("")]
		public virtual Register9ViewModel Create([FromBody]Register9ViewModel register)
		{
			Register9Service service = new Register9Service();
			var entity = Mapper.Map<Register9>(register);
			var result = service.Create(entity);
			return Mapper.Map<Register9ViewModel>(result);
		}

		[HttpPost, Route("createRegisterData")]
		public virtual void CreateRegisterData([FromBody]Register9DataViewModel register)
		{
			Register9Service service = new Register9Service();
			var entity = Mapper.Map<Register9Data>(register);
			var result = service.CreateRegisterData(entity);
			return;
		}

		[HttpPost, Route("editRegisterData")]
		public virtual void EditRegisterData([FromBody]Register9DataViewModel register)
		{
			Register9Service service = new Register9Service();
			var entity = Mapper.Map<Register9Data>(register);
			var result = service.EditRegisterData(entity);
			return;
		}

		[HttpPost, Route("deleteRegisterData")]
		public virtual void DeleteRegisterData([FromBody]Register9DataViewModel register)
		{
			Register9Service service = new Register9Service();
			//var entity = Mapper.Map<Register9Data>(register);
			var result = service.DeleteRegisterData(register.Id);
			return;
		}

		//[HttpPost, Route("calculate")]
		// public Register8ViewModel Calculate([FromBody]Register8ViewModel model)
		//  {
		//      var entity = Mapper.Map<Register8>(model);
		//      return Mapper.Map<Register8ViewModel>(entity);
		// }
	}
}
