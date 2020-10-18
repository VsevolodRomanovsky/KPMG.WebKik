using System.Web.Http;
using KPMG.WebKik.Contracts.Service.Registers;
using AutoMapper;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Web.Controllers.Register
{
    [RoutePrefix("api/register6")]
    public class Register6Controller : EntityController<Register6, Register6ViewModel, int>
    {
        public Register6Controller(IRegister6Service service) : base(service)
        {
        }

        [HttpPost, Route("calculate")]
        public Register6ViewModel Calculate([FromBody]Register6ViewModel model)
        {
            var entity = Mapper.Map<Register6>(model);
            entity = (Service as IRegister6Service).CalculateRegisterFields(entity);
            return Mapper.Map<Register6ViewModel>(entity);
        }
    }
}
