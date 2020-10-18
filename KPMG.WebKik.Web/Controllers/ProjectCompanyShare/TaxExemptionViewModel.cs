using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPMG.WebKik.Web.Controllers.ProjectCompanyShare
{
    public class TaxExemptionViewModel : IEntity<int>
    {
        public int Id { get; set; }

        public RationalyType[] Rationaly { get; set; }

        public int Year { get; set; }
        

        public int OwnerProjectCompanyId { get; set; }
        //public ProjectCompanyViewModel OwnerProjectCompany { get; set; }
        public int DependentProjectCompanyId { get; set; }
       // public ProjectCompanyViewModel DependentProjectCompany { get; set; }


        public bool? Result { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<TaxExemption, TaxExemptionViewModel>();

            cfg.CreateMap<TaxExemptionViewModel, TaxExemption>()
                .ForMember(t => t.Rationalities, t => t.Ignore());
        }
    }
}