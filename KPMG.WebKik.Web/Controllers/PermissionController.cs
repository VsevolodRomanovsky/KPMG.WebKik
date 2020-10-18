using System.Web.Http;
using System.Threading.Tasks;
using KPMG.WebKik.Models;
using KPMG.WebKik.Contracts.Service;

namespace KPMG.WebKik.Web.Controllers
{
    [RoutePrefix("api/permissions")]
    public class PermissionController : BaseController
    {
        public PermissionController(IPermissionService service) : base(service)
        {
        }

        [HttpGet, Route("current")]
        public async Task<Permission> GetCurrentUserPermission()
        {
            return await ((IPermissionService) Service).GetCurrentUserPermission();
        }

        protected override void Dispose(bool disposing)
        {
            ((IPermissionService) Service).Dispose();
            base.Dispose(disposing);
        }
    }
}
