using System;

namespace KPMG.WebKik.Models.ProjectCompanies
{
    public class ProjectCompanyControl : IEntity<int>
    {
        public int Id { get; set; }

        public ProjectCompany ProjectCompany { get; set; }

        public bool IsFounder { get; set; }

        public bool IsControlledBy { get; set; }

        public DateTimeOffset FoundDate { get; set; }

        public bool IsOwnInterest { get; set; }
        public bool IsPartnerInterest { get; set; }
        public bool IsChildInterest { get; set; }
    }
}
