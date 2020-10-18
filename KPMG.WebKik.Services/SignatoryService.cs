using System.Collections.Generic;
using System.Threading.Tasks;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Models;
using KPMG.WebKik.Contracts.Repository;

namespace KPMG.WebKik.Services
{
    public class SignatoryService : EntityService<Signatory, int>, ISignatoryService
    {
        public SignatoryService(IEntityRepository<Signatory, int> repository) : base(repository)
        {
        }

        public async Task<Signatory> GetSignatureById(int id)
        {
            return await repository
                .Where(x => x.Id == id)
                .Include(x => x.SignatoryCode)
                .Include(x => x.ConfirmationDocument)
                .FirstAsync();
        }

        public async Task<IEnumerable<Signatory>> GetByCompanyId(int companyId)
        {
            return await repository
                .Where(x => x.ProjectCompanyId == companyId)
                .Include(x => x.SignatoryCode)
                .ToListAsync();
        }

    }
}
