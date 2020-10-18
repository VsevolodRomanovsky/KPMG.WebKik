using System.Web.Http;
using KPMG.WebKik.Contracts.Service.Registers;
using AutoMapper;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Web.Controllers.Register
{
    [RoutePrefix("api/register7")]
    public class Register7Controller : EntityController<Register7, Register7ViewModel, int>
    {
        public Register7Controller(IRegister7Service service) : base(service)
        {
        }

        [HttpPost, Route("calculate")]
        public Register7ViewModel Calculate([FromBody]Register7ViewModel model)
        {
            var entity = Mapper.Map<Register7>(model);
            entity = (Service as IRegister7Service).CalculateRegisterFields(entity);
            return Mapper.Map<Register7ViewModel>(entity);
        }
    }
}
