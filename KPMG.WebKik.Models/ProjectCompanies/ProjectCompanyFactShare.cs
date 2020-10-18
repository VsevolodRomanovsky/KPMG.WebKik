namespace KPMG.WebKik.Models.ProjectCompanies
{
    public class ProjectCompanyFactShare
    {
        public bool IsNotificationOfParticipationRequired;
        

        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompany OwnerProjectCompany { get; set; }
        public int DependentProjectCompanyId { get; set; }
        public ProjectCompany DependentProjectCompany { get; set; }
        public double ShareFactPart { get; set; }
        public double ShareDirectPart { get; set; }

        public bool IsFounder { get; set; }

        public bool IsControlledBy { get; set; }
        public bool IsOwnInterest { get; set; }
        public bool IsPartnerInterest { get; set; }
        public bool IsChildInterest { get; set; }
        public ProjectCompanyShare[] DirectShares { get; set; }
    }
}
