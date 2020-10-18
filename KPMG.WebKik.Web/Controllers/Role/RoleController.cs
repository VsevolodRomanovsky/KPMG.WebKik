using System.Web.Http;
using KPMG.WebKik.Contracts.Service;

namespace KPMG.WebKik.Web.Controllers.Role
{
    [Authorize(Roles = Models.Role.Administrator)]
    [RoutePrefix("api/roles")]
    public class RoleController : EntityController<Models.Role, RoleViewModel, int>
    {
        public RoleController(IEntityService<Models.Role, int> entityService) : base(entityService)
        { }
    }
}
