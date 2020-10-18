using KPMG.Webkik.Utils;
using System.Collections.Generic;
using System.Linq;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models.ProjectCompany
{
    public static class ModelHelper
    {
        public static void AddShares(this ProjectCompanies.ProjectCompany company, IEnumerable<ProjectCompanyShare> shares)
        {
            company.OwnerProjectCompanyShares.AddRange(shares.Where(s => s.OwnerProjectCompanyId == company.Id));
            company.DependentProjectCompanyShares.AddRange(shares.Where(s => s.DependentProjectCompanyId == company.Id));
        }
     
        public static void AddShares(this IEnumerable<ProjectCompanies.ProjectCompany> companies, IEnumerable<ProjectCompanyShare> shares)
        {
            foreach (var company in companies)
            {
                company.AddShares(shares);
            }
        }
    }
}
