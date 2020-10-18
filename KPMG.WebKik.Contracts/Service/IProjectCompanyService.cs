using System.Collections.Generic;
using System.Threading.Tasks;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Companies;

namespace KPMG.WebKik.Contracts.Service
{
    public interface IProjectCompanyService : IEntityService<ProjectCompany, int>
    {
        Task<IList<ProjectCompany>> GetAllByProjectId(int projectId);

        Task CalculateProjectInfo(int projectId);

       // Task<byte[]> GetKIKDocument(int companyId, string path, int year);

        Task<IEnumerable<ProjectCompany>> GetCompaniesForNotification(int projectId);


        TaxExemption TaxExemptionFor(int ownerCompanyId, int companyId, int year);

        Task<TaxExemptionResult> DefineTaxStatus(TaxExemption entity);
		SupportingDocument UploadFile(int year, int companyType, int companyId, bool isUU, bool isUKIK, bool isND, string uKIKDocType, byte[] fileData, string fileName);

        void UploadDataFromExcel(byte[] file, int sharedId, int year);

        void IndividualUploadDataFromExcel(byte[] file, int projectId);

        void ForeignUploadDataFromExcel(byte[] file, int projectId);

        void ForeignLightUploadDataFromExcel(byte[] file, int projectId);

        void DomesticUploadDataFromExcel(byte[] file, int projectId);

        void UploadKlDataFromExcel(byte[] file, int sharedId, int year);
        IList<IndividualCompany> GetIndividualCompId(int id);
    }
}
