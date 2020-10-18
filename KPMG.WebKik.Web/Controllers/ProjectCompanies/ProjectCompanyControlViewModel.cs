using System;
using AutoMapper.Configuration;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Web.Automapper;

namespace KPMG.WebKik.Web.Controllers.ProjectCompanies
{
    public class ProjectCompanyControlViewModel
    {
        public int Id { get; set; }

        public ProjectCompanyViewModel ProjectCompany { get; set; }

        public bool IsFounder { get; set; }

        public bool IsControlledBy { get; set; }

        public DateTimeOffset FoundDate { get; set; }

        public bool IsOwnInterest { get; set; }
        public bool IsPartnerInterest { get; set; }
        public bool IsChildInterest { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProjectCompanyControl, ProjectCompanyControlViewModel>();
            cfg.CreateMap<ProjectCompanyControlViewModel, ProjectCompanyControl>().MaxDepth(3);
        }
    }
}