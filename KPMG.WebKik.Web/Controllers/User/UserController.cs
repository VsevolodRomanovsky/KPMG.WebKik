using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using KPMG.WebKik.Contracts.Service;

namespace KPMG.WebKik.Web.Controllers.User
{
    [RoutePrefix("api/users")]
    public class UserController : EntityController<Models.User, UserViewModel, int>
    {
        public UserController(IUserService entityService) : base(entityService)
        {
        }

        [HttpGet, Route("project/{projectId}")]
        public async Task<IQueryable<UserViewModel>> GetUsersByProjectId(int projectId)
        {
            var result = await ((IUserService)Service).GetUsersByProjectId(projectId);
            return result.AsQueryable().ProjectTo<UserViewModel>();
        }

        [Authorize(Roles = Models.Role.Administrator)]
        [HttpPost, Route("project/{projectId}/add/{userId}")]
        public async Task<UserViewModel> AddUserToProject(int projectId, int userId)
        {
            var entity = await ((IUserService)Service).AddUserToProject(projectId, userId);
            return Mapper.Map<UserViewModel>(entity);
        }

        [Authorize(Roles = Models.Role.Administrator)]
        [HttpDelete, Route("project/{projectId}/remove/{userId}")]
        public async Task RemoveUserFromProject(int projectId, int userId)
        {
            await ((IUserService)Service).RemoveUserFromProject(projectId, userId);
        }
    }
}
