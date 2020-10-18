using KPMG.WebKik.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KPMG.WebKik.Contracts.Service
{
    public interface ISignatoryService : IEntityService<Signatory, int>
    {
        Task<Signatory> GetSignatureById(int id);
        Task<IEnumerable<Signatory>> GetByCompanyId(int companyId);
    }
}
