using System;
using System.Collections.Generic;
using System.Linq;
using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Models.ProjectCompanies
{
    public class ProjectCompany : IEntity<int>
    {
        public ProjectCompany()
        {
            OwnerProjectCompanyShares = new HashSet<ProjectCompanyShare>();
            DependentProjectCompanyShares = new HashSet<ProjectCompanyShare>();
            //OwnerProjectCompanyFactShares = new HashSet<ProjectCompanyFactShare>();
            //DependentProjectCompanyFactShares = new HashSet<ProjectCompanyFactShare>();
            Signatories = new HashSet<Signatory>();
            Notifications = new HashSet<NotificationOfParticipation>();
            TaxReturns = new HashSet<TaxReturn>();
            NotificationOfKIKs = new HashSet<NotificationOfKIK>();
            TaxExemptions = new HashSet<TaxExemption>();
            Registers1 = new HashSet<Register1>();
            Registers2 = new HashSet<Register2>();
            Registers3 = new HashSet<Register3>();
            Registers4 = new HashSet<Register4>();
            Registers5 = new HashSet<Register5>();
            Registers6 = new HashSet<Register6>();
            Registers7 = new HashSet<Register7>();
			Registers8 = new HashSet<Register8>();
			Registers9 = new HashSet<Register9>();
            Registers10 = new HashSet<Register10>();
			SupportingDocuments = new HashSet<SupportingDocument>();
		}

        public int Id { get; set; }
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string Name { get; set; }

        public State State { get; set; }

        public bool IsResident { get; set; }

        public virtual ForeignCompany ForeignCompany { get; set; }

        public virtual ForeignLightCompany ForeignLightCompany { get; set; }

        public virtual IndividualCompany IndividualCompany { get; set; }

        public virtual DomesticCompany DomesticCompany { get; set; }

        public virtual ProjectCompanyControl ProjectCompanyControl { get; set; }

        public ICollection<ProjectCompanyShare> OwnerProjectCompanyShares { get; set; }
        public ICollection<ProjectCompanyShare> DependentProjectCompanyShares { get; set; }

        //public ICollection<ProjectCompanyFactShare> OwnerProjectCompanyFactShares { get; set; }
        //public ICollection<ProjectCompanyFactShare> DependentProjectCompanyFactShares { get; set; }

        public string ExemptReason { get; set; }
        public bool? IsTaxExempt { get; set; }

        public bool IsControlCompany{ get; set; }

        public bool IsKIKCompany { get; set; }

        public ICollection<NotificationOfParticipation> Notifications { get; set; }
        public ICollection<TaxReturn> TaxReturns { get; set; }
        public ICollection<NotificationOfKIK> NotificationOfKIKs { get; set; }
        public ICollection<Signatory> Signatories { get; set; }

        public ICollection<TaxExemption> TaxExemptions { get; set; }

        public ICollection<Register1> Registers1 { get; set; }
        public ICollection<Register2> Registers2 { get; set; }
        public ICollection<Register3> Registers3 { get; set; }
        public ICollection<Register4> Registers4 { get; set; }
        public ICollection<Register5> Registers5 { get; set; }
        public ICollection<Register6> Registers6 { get; set; }
        public ICollection<Register7> Registers7 { get; set; }
		public ICollection<Register8> Registers8 { get; set; }

		public ICollection<Register9> Registers9 { get; set; }
        public ICollection<Register10> Registers10 { get; set; }

		public ICollection<Register11> Registers11 { get; set; }

		public ICollection<SupportingDocument> SupportingDocuments { get; set; }
	}
}
