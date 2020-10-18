using System;
using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.Directory;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;

namespace KPMG.WebKik.Web.Controllers.Company
{
    public class ForeignLightCompanyViewModel : IEntity<int>
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int ForeignOrganizationalFormCodeId { get; set; }
        public string EnglishName { get; set; }
        public string RussianName { get; set; }
        public DateTimeOffset FoundDate { get; set; }
        public string RequisitesEng { get; set; }
        public string RequisitesRus { get; set; }
        public CountryCodeViewModel CountryCode { get; set; }
        public int CountryCodeId { get; set; }
        public string RegNumber { get; set; }
        public string OtherInfo { get; set; }
        public ProjectCompanyViewModel ProjectCompany { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ForeignLightCompany, ForeignLightCompanyViewModel>()
                .ForMember(x => x.ProjectCompany, y => y.Ignore());
            cfg.CreateMap<ForeignLightCompanyViewModel, ForeignLightCompany>()
                .ForMember(x => x.ForeignOrganizationalFormCode, y => y.Ignore())
                .ForMember(x => x.ProjectCompany, y => y.Ignore());
        }
    }
}