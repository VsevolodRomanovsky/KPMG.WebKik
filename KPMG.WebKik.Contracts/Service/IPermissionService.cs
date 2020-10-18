using KPMG.WebKik.Models;
using System.Threading.Tasks;

namespace KPMG.WebKik.Contracts.Service
{
    public interface IPermissionService : IBaseService
    {
        Task<Permission> GetCurrentUserPermission();
    }
}
