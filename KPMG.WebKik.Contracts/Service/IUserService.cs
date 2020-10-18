using KPMG.WebKik.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KPMG.WebKik.Contracts.Service
{
    public interface IUserService : IEntityService<User, int>
    {
        Task<IList<User>> GetUsersByProjectId(int projectId);
        Task<User> AddUserToProject(int projectId, int userId);
        Task RemoveUserFromProject(int projectId, int userId);
    }
}
