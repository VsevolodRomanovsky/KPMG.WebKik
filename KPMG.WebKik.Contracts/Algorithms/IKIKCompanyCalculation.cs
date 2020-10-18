using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Contracts.Algorithms
{
    public interface IKIKCompanyCalculation
    {
        bool IsKIKCompany(ProjectCompanyFactShare share);
    }
}
