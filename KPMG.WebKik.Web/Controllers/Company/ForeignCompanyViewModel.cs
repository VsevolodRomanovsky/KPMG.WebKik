using System;
using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.Directory;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;

namespace KPMG.WebKik.Web.Controllers.Company
{
    public class ForeignCompanyViewModel : IEntity<int>
    {
        public int Id { get; set; }
        public ProjectCompanyViewModel ProjectCompany { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public int CountryCodeId { get; set; }
        public CountryCodeViewModel CountryCode { get; set; }
        public string RegistrationNumber { get; set; }
        public int? TaxPayerCodeId { get; set; }
        public string Address { get; set; }
        public DateTimeOffset FoundDate { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ForeignCompany, ForeignCompanyViewModel>()
                .ForMember(x => x.ProjectCompany, y => y.Ignore());
            cfg.CreateMap<ForeignCompanyViewModel, ForeignCompany>()
                .ForMember(x => x.TaxPayerCode, y => y.Ignore())
                .ForMember(x => x.ProjectCompany, y => y.Ignore());
        }
    }
}