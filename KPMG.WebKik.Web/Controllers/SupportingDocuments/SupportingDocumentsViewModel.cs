using System;
using AutoMapper.Configuration;
using KPMG.Webkik.Utils;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.Company;
using KPMG.WebKik.Web.Controllers.Project;

namespace KPMG.WebKik.Web.Controllers.SupportingDocuments
{
    public class SupportingDocumentsViewModel : IEntity<int>
    {
        public int Id { get; set; }

		public string FileName { get; set; }

		public int ProjectCompanyId { get; set; }

		public bool IsUU { get; set; }

		public bool IsUKIK { get; set; }

		public bool IsND { get; set; }

		public string UKIKDocType { get; set; }

		//public ProjectCompany ProjectCompany { get; set; }


        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
			cfg.CreateMap<SupportingDocument, SupportingDocumentsViewModel>()
						.ForMember(m => m.Id, o => o.MapFrom(s => s.Id))
			.ForMember(m => m.ProjectCompanyId, o => o.MapFrom(s => s.ProjectCompanyId))
			.ForMember(m => m.IsUU, o => o.MapFrom(s => s.IsUU))
			.ForMember(m => m.IsUKIK, o => o.MapFrom(s => s.IsUKIK))
			.ForMember(m => m.IsND, o => o.MapFrom(s => s.IsND))
			.ForMember(m => m.UKIKDocType, o => o.MapFrom(s => s.UKIKDocType))
			//.ForMember(m => m.ProjectCompany, o => o.MapFrom(s => s.ProjectCompany))
			.ForMember(m => m.FileName, o => o.MapFrom(s => s.FileName))
			.ForAllOtherMembers(m => m.Ignore());
		

		cfg.CreateMap<SupportingDocumentsViewModel, SupportingDocument>()
			.ForMember(m => m.Id, o => o.MapFrom(s => s.Id))
			.ForMember(m => m.ProjectCompanyId, o => o.MapFrom(s => s.ProjectCompanyId))
			.ForMember(m => m.IsUU, o => o.MapFrom(s => s.IsUU))
			.ForMember(m => m.IsUKIK, o => o.MapFrom(s => s.IsUKIK))
			.ForMember(m => m.IsND, o => o.MapFrom(s => s.IsND))
			.ForMember(m => m.UKIKDocType, o => o.MapFrom(s => s.UKIKDocType))
			//.ForMember(m => m.ProjectCompany, o => o.MapFrom(s => s.ProjectCompany))
			.ForMember(m => m.FileName, o => o.MapFrom(s => s.FileName))
			.ForAllOtherMembers(m => m.Ignore());
		}
	}
}
