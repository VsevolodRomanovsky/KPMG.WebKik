using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Models;
using System.Collections.Generic;

namespace KPMG.WebKik.Web.Controllers.User
{
    [RoutePrefix("api/signatories")]
    public class SignatoryController : EntityController<Signatory, SignatoryViewModel, int>
    {
        public SignatoryController(ISignatoryService entityService) : base(entityService)
        {
        }

        [HttpGet, Route("company/{companyId}")]
        public async Task<IEnumerable<SignatoryViewModel>> GetByCompanyId(int companyId)
        {
            var result = await ((ISignatoryService)Service).GetByCompanyId(companyId);
            return result.Select(x => Mapper.Map<SignatoryViewModel>(x));
        }
    }
}
