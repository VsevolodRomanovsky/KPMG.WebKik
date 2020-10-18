using KPMG.WebKik.Models.ProjectCompanies;
using System.Collections.Generic;

namespace KPMG.WebKik.Contracts.Algorithms
{
    public interface IControlCompanyCalculation
    {
        bool IsControlCompany(ProjectCompanyShare share, IList<ProjectCompany> companies, IList<ProjectCompanyFactShare> factShares);
    }
}
