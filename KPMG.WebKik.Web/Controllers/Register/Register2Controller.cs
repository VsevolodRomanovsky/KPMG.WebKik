using System.Web.Http;
using KPMG.WebKik.Contracts.Service.Registers;
using AutoMapper;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Web.Controllers.Register
{
    [RoutePrefix("api/register2")]
    public class Register2Controller : EntityController<Register2, Register2ViewModel, int>
    {
        public Register2Controller(IRegister2Service service) : base(service)
        {
        }

        [HttpPost, Route("calculate")]
        public Register2ViewModel Calculate([FromBody]Register2ViewModel model)
        {
            var entity = Mapper.Map<Register2>(model);
            entity = (Service as IRegister2Service).CalculateRegisterFields(entity);
            return Mapper.Map<Register2ViewModel>(entity);
        }
    }
}
