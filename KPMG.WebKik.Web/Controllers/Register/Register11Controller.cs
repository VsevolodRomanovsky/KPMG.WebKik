using System.Web.Http;
using KPMG.WebKik.Contracts.Service.Registers;
using AutoMapper;
using KPMG.WebKik.Models.Registers;
using System.Threading.Tasks;
using KPMG.WebKik.Services.Registers;

namespace KPMG.WebKik.Web.Controllers.Register
{
    [RoutePrefix("api/register11")]
    public class Register11Controller : ApiController
	{
		private IRegister11Service service;

		//this.service = 
		public Register11Controller(IRegister11Service service)
        {
			this.service = service;
		}

		[HttpGet, Route("{id:int}")]
		public Register11ViewModel GetById(int id)
		{
			//Register9Service service = new Register9Service();
			var result = (service as IRegister11Service).GetRegister11(id); ;
			return Mapper.Map<Register11ViewModel>(result);
		}

		//[HttpGet, Route("company/{companyId}")]
		//public async Task<IQueryable<ProjectCompanyShareViewModel>> GetSharesByCompanyId(int companyId)
		//{
		//	var companies = await (Service as IProjectCompanyShareService).GetAllByProjectCompanyId(companyId);
		//	return companies.AsQueryable().ProjectTo<ProjectCompanyShareViewModel>();
		//}

		[HttpPost, Route("")]
		public virtual Register11ViewModel Create([FromBody]Register11ViewModel register)
		{
			var entity = Mapper.Map<Register11>(register);
			var result = (service as IRegister11Service).Create(entity);
			return Mapper.Map<Register11ViewModel>(result);
		}

		[HttpPost, Route("createRegisterData")]
		public virtual void CreateRegisterData([FromBody]Register11DataViewModel register)
		{
			var entity = Mapper.Map<Register11Data>(register);
			var result = (service as IRegister11Service).CreateRegisterData(entity);
			return;
		}

		[HttpPost, Route("editRegisterData")]
		public virtual void EditRegisterData([FromBody]Register11DataViewModel register)
		{
			var entity = Mapper.Map<Register11Data>(register);
			var result = (service as IRegister11Service).EditRegisterData(entity);
			return;
		}

		[HttpPost, Route("deleteRegisterData")]
		public virtual void DeleteRegisterData([FromBody]Register11DataViewModel register)
		{
			var result = (service as IRegister11Service).DeleteRegisterData(register.Id);
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
