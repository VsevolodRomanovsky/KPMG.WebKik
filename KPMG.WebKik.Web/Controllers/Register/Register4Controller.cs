using System.Web.Http;
using KPMG.WebKik.Contracts.Service.Registers;
using AutoMapper;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Web.Controllers.Register
{
    [RoutePrefix("api/register4")]
    public class Register4Controller : EntityController<Register4, Register4ViewModel, int>
    {
        public Register4Controller(IRegister4Service service) : base(service)
        {
        }

        [HttpPost, Route("calculate")]
        public Register4ViewModel Calculate([FromBody]Register4ViewModel model)
        {
            var entity = Mapper.Map<Register4>(model);
            entity = (Service as IRegister4Service).CalculateRegisterFields(entity);
            return Mapper.Map<Register4ViewModel>(entity);
        }
    }
}
