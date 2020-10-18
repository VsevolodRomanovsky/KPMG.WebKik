using KPMG.WebKik.Models.Registers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace KPMG.WebKik.Models.ProjectCompanies
{
    public class ProjectCompanyShare : IEntity<int>
    {

        public ProjectCompanyShare()
        {
            
        }
        
        public int Id { get; set; }

        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompany OwnerProjectCompany { get; set; }
        public int DependentProjectCompanyId { get; set; }
        public ProjectCompany DependentProjectCompany { get; set; }

        public string CompanyStatus { get; set; }

        public ShareType ShareType { get; set; }

        public double SharePart { get; set; }

        [Column(TypeName = "Date")]
        public DateTime ShareStartDate { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? ShareFinishDate { get; set; }

        public double? ShareWithResidentsPart { get; set; }

        public double? ShareWithFamilyPart { get; set; }

        public bool? IsFounder { get; set; }

        public bool? IsControlledBy { get; set; }

        public bool? IsIndependentRecognition { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? FoundDate { get; set; }

        public bool? IsOwnInterest { get; set; }
        public bool? IsPartnerInterest { get; set; }
        public bool? IsChildInterest { get; set; }

        [Column(TypeName = "Date")]
        public DateTime? ControlEmergenceDate { get; set; }
        public bool? IsNotificationOfParticipationRequired { get; set; }
        public GroundsType? ForeignLightCompanyGrounds { get; set; }
        public string ParticipantStatus { get; set; }
        public string ControlGrounds { get; set; }

       
    }
}
