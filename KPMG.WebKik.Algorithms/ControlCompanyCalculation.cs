using System.Linq;
using KPMG.WebKik.Contracts.Algorithms;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Contracts.Service;
using System.Collections.Generic;
using System;

namespace KPMG.WebKik.Algorithms
{
    public class ControlCompanyCalculation : IControlCompanyCalculation
    {
        public bool CompanyHasControlValues(ProjectCompanyShare share)
        {
            return true.Equals(share.IsControlledBy);//.HasValue && share.IsControlledBy.Value;
        }

        public bool CompanyHasLargeFactShare(ProjectCompanyShare share)
        {
            return share.SharePart > 25.0;
        }
        public bool CompanyHasMoreThenDomesticFactShare(ProjectCompanyShare share, IList<ProjectCompany> companies, IList<ProjectCompanyFactShare> factShares)
        {

            var dependentCompany = companies.Where(c => c.Id == share.DependentProjectCompanyId).First();

            if (dependentCompany.State == State.Individual || dependentCompany.State == State.Domestic)
                return false;

            var fshare = factShares.Where(f => dependentCompany.DependentProjectCompanyShares.Any(d => d.OwnerProjectCompanyId == f.OwnerProjectCompanyId &&
            d.OwnerProjectCompany.IsResident)).ToList();
            var sumShareFactPart = fshare.Sum(f => f.ShareFactPart);

            return share.SharePart > 10.0 && sumShareFactPart > 50.0;


        }

        public bool IsControlCompany(ProjectCompanyShare share, IList<ProjectCompany> companies, IList<ProjectCompanyFactShare> factShares)
        {
            return (CompanyHasControlValues(share) || CompanyHasLargeFactShare(share) || CompanyHasMoreThenDomesticFactShare(share, companies, factShares));
        }
    }
}
