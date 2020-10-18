using KPMG.WebKik.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KPMG.WebKik.Contracts.Service
{
    public interface ITaxReturnService : IEntityService<TaxReturn, int>
    {
        Task<IEnumerable<TaxReturn>> GetByProjectId(int projectId);

        Task<byte[]> GetDocument(int companyId, string path, int year);
    }
}
