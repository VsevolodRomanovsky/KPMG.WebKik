using System;
using AutoMapper.Configuration;
using KPMG.Webkik.Utils;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.Company;
using KPMG.WebKik.Web.Controllers.Project;
using System.Collections.Generic;
using KPMG.WebKik.Web.Controllers.SupportingDocuments;

namespace KPMG.WebKik.Web.Controllers.ProjectCompanies
{
    public class ProjectCompanyViewModel : IEntity<int>
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        public ProjectViewModel Project { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public string Name { get; set; }

        public State State { get; set; }
        public string StateName { get; set; }

        public bool IsResident { get; set; }
        public bool IsControlCompany { get; set; }
        public bool IsKIKCompany { get; set; }
        public string ExemptReason { get; set; }
        public bool? IsTaxExempt { get; set; }


        public DomesticCompanyViewModel DomesticCompany { get; set; }
        public ForeignCompanyViewModel ForeignCompany { get; set; }

        public ForeignLightCompanyViewModel ForeignLightCompany { get; set; }

        public IndividualCompanyViewModel IndividualCompany { get; set; }

		public ICollection<SupportingDocumentsViewModel> SupportingDocuments { get; set; }

		[AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProjectCompany, ProjectCompanyViewModel>()
                .ForMember(x => x.StateName, o => o.MapFrom(s => s.State.GetDescription()))
				.ForMember(m => m.SupportingDocuments, o => o.MapFrom(s => s.SupportingDocuments));
			;

            cfg.CreateMap<ProjectCompanyViewModel, ProjectCompany>()
                .ForMember(x => x.ProjectCompanyControl, y => y.Ignore())
                .ForMember(x => x.OwnerProjectCompanyShares, y => y.Ignore())
                .ForMember(x => x.DependentProjectCompanyShares, y => y.Ignore())
                .ForMember(x => x.Signatories, y => y.Ignore())
                .ForMember(x => x.Notifications, y => y.Ignore())
                .ForMember(x => x.Registers1, o => o.Ignore())
                .ForMember(x => x.Registers2, o => o.Ignore())
                .ForMember(x => x.Registers3, o => o.Ignore())
                .ForMember(x => x.Registers4, o => o.Ignore())
                .ForMember(x => x.Registers5, o => o.Ignore())
                .ForMember(x => x.Registers6, o => o.Ignore())
                .ForMember(x => x.Registers7, o => o.Ignore())
				.ForMember(x => x.Registers8, o => o.Ignore())
                .ForMember(x => x.Registers10, o => o.Ignore())
				.ForMember(x => x.Registers9, o => o.Ignore())
				.ForMember(x => x.Registers11, o => o.Ignore())
				.ForMember(x => x.TaxExemptions, o => o.Ignore())
                .ForMember(x => x.TaxReturns, o => o.Ignore())
                .ForMember(x => x.NotificationOfKIKs, o => o.Ignore())
				.ForMember(m => m.SupportingDocuments, o => o.MapFrom(s => s.SupportingDocuments));
			;
        }
    }
}
