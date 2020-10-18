using System.Threading.Tasks;
using KPMG.WebKik.Models;

namespace KPMG.WebKik.Contracts.Service
{
    public interface IProjectService : IEntityService<Project, int>
    {
        Task<Project> GetProjectOwnership(int id);
    }
}
