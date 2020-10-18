using System.Threading.Tasks;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Models;
using System.Security.Authentication;
using KPMG.WebKik.Contracts.Repository;

namespace KPMG.WebKik.Services
{
    public class PermissionService : BaseService, IPermissionService
    {
        private readonly IEntityRepository<User, int> repository;
        public PermissionService(IEntityRepository<User, int> repository)
        {
            this.repository = repository;
        }

        public async Task<Permission> GetCurrentUserPermission()
        {
            var userLogin = Identity.Name;
            var user = await repository
                .Where(x => !x.IsDisabled && x.UserLogin == userLogin)
                .Include(x => x.Role).SingleOrDefaultAsync();
            if (user == null)
            {
                throw new AuthenticationException();
            }
            return new Permission { IsAdmin = user.Role.Name == Role.Administrator };
        }

        public override void Dispose()
        {
            repository.Dispose();
        }
    }
}
