using System.Web.Http;
using KPMG.WebKik.Contracts.Service.Registers;
using AutoMapper;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Web.Controllers.Register
{
    [RoutePrefix("api/register3")]
    public class Register3Controller : EntityController<Register3, Register3ViewModel, int>
    {
        public Register3Controller(IRegister3Service service) : base(service)
        {
        }

        [HttpPost, Route("calculate")]
        public Register3ViewModel Calculate([FromBody]Register3ViewModel model)
        {
            var entity = Mapper.Map<Register3>(model);
            entity = (Service as IRegister3Service).CalculateRegisterFields(entity);
            return Mapper.Map<Register3ViewModel>(entity);
        }
    }
}
