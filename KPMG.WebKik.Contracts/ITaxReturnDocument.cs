using KPMG.WebKik.Models;
using KPMG.WebKik.Models.ProjectCompanies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KPMG.WebKik.Contracts.Service
{
    public interface ITaxReturnDocument
    {
        Task<byte[]> GetDocumentData(ProjectCompany ownerCompany, IList<ProjectCompanyFactShare> factShares, string templatePath, int year);
    }
}
