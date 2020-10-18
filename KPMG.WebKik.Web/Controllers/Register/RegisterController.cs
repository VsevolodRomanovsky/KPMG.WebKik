using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KPMG.WebKik.Contracts.Service;
using System.Threading.Tasks;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Contracts.Service.Registers;

namespace KPMG.WebKik.Web.Controllers.Register
{
    [RoutePrefix("api/register")]
    public class RegisterController : ApiController
    {
        private readonly IRegisterService registerService;
        public RegisterController(IRegisterService registerService)
        {
            this.registerService = registerService;
        }

        [HttpGet, Route("company/{companyId}")]
        public async Task<IEnumerable<RegisterListItem>> GetRegistersByCompanyId(int companyId, int year)
        {
            return await registerService.GetRegistersByShareIdAndYear(companyId, year);
        }

        [HttpGet, Route("years")]
        public IEnumerable<int> GetGroundTypes()
        {
            return Enum.GetValues(typeof(Year)).Cast<int>();
        }

    }
}
