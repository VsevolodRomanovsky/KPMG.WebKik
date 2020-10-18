using System.Web.Http;
using KPMG.WebKik.Contracts.Service.Registers;
using AutoMapper;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Web.Controllers.Register
{
    [RoutePrefix("api/register1")]
    public class Register1Controller : EntityController<Register1, Register1ViewModel, int>
    {
        public Register1Controller(IRegister1Service service) : base(service)
        {
        }

        [HttpPost, Route("calculate")]
        public Register1ViewModel Calculate([FromBody]Register1ViewModel model)
        {
            var entity = Mapper.Map<Register1>(model);
            entity = (Service as IRegister1Service).CalculateRegisterFields(entity);
            return Mapper.Map<Register1ViewModel>(entity);
        }
    }
}
