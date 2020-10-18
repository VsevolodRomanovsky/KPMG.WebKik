using System.Web.Http;
using KPMG.WebKik.Contracts.Service.Registers;
using AutoMapper;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Web.Controllers.Register
{
    [RoutePrefix("api/register5")]
    public class Register5Controller : EntityController<Register5, Register5ViewModel, int>
    {
        public Register5Controller(IRegister5Service service) : base(service)
        {
        }

        [HttpPost, Route("calculate")]
        public  Register5ViewModel Calculate([FromBody]Register5ViewModel model)
        {
            var entity = Mapper.Map<Register5>(model);
            entity = (Service as IRegister5Service).CalculateRegisterFields(entity);
            return Mapper.Map<Register5ViewModel>(entity);
        }
    }
}
