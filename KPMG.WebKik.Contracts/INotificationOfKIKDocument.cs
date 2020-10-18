using KPMG.WebKik.Models;
using KPMG.WebKik.Models.ProjectCompanies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KPMG.WebKik.Contracts.Service
{
    public interface INotificationOfKIKDocument
    {

        Task<byte[]> GetDocumentData(ProjectCompany ownerCompany, IList<ProjectCompanyFactShare> factShares, Signatory signature, string templatePath, int year, int correction, int taxAuthorityCode);

    }
}
