using AutoMapper.Configuration;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPMG.WebKik.Web.Controllers.ProjectCompanyShare
{
    public class ProjectCompanyFactShareViewModel
    {
        public ProjectCompanyViewModel DependentProjectCompany { get; set; }

        public double ShareFactPart { get; set; }
        public double ShareDirectPart { get; set; }
        public double ShareIndirectPart { get { return ShareFactPart - ShareDirectPart; } }

        public string ShareType
        {
            get
            {
                if (ShareDirectPart > 0 && ShareIndirectPart > 0) return "Смешанное";
                if (ShareDirectPart > 0) return "Прямое";
                return "Косвенное";
            }
        }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ProjectCompanyFactShare, ProjectCompanyFactShareViewModel>();

            //cfg.CreateMap<Models.ProjectCompanies.ProjectCompanyFactShare, ProjectCompanyFactShareViewModel>();
            //cfg.CreateMap<ProjectCompanyFactShareViewModel, Models.ProjectCompanies.ProjectCompanyShare>();
        }
    }
}