using System.Collections.Generic;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Contracts.Algorithms
{
    public interface IFactShareCalculation
    {
        IList<ProjectCompanyFactShare> GetFactShares(IEnumerable<ProjectCompanyShare> companyShares);            
    }
}
