using System.Linq;
using KPMG.WebKik.Contracts.Algorithms;
using KPMG.WebKik.Models.ProjectCompanies;
namespace KPMG.WebKik.Algorithms
{
    public class KIKCompanyCalculation : IKIKCompanyCalculation
    {
        public bool IsCompanyForegin(ProjectCompanyFactShare share)
        {
            return (share.DependentProjectCompany.State == State.ForeignLight || share.DependentProjectCompany.State == State.Foreign);
        }

        public bool IsCompanyNotResident(ProjectCompanyFactShare share)
        {
            return !share.DependentProjectCompany.IsResident;
        }
        public bool IsCompanyControlFaceIsResident(ProjectCompanyFactShare share)
        {
            return share.OwnerProjectCompany.IsControlCompany && share.OwnerProjectCompany.IsResident;
        }

        public bool IsKIKCompany(ProjectCompanyFactShare share)
        {
            if (share.OwnerProjectCompany.State == State.Domestic && share.OwnerProjectCompany.DomesticCompany.IsPublic)
                return false;

            return (IsCompanyForegin(share) && IsCompanyNotResident(share) && IsCompanyControlFaceIsResident(share));
        }
    }
}
