using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;

namespace KPMG.WebKik.Web.Controllers.Company
{
    public class DomesticCompanyViewModel : IEntity<int>
    {
        public int Id { get; set; }
        public ProjectCompanyViewModel ProjectCompany { get; set; }
        public string Number { get; set; }
        public string FullName { get; set; }
        public long OGRN { get; set; }
        public long INN { get; set; }
        public string KPP { get; set; }
        public bool IsPublic { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<DomesticCompany, DomesticCompanyViewModel>()
                .ForMember(x => x.ProjectCompany, y => y.Ignore());
            cfg.CreateMap<DomesticCompanyViewModel, DomesticCompany>()
                .ForMember(x => x.ProjectCompany, y => y.Ignore());
        }
    }
}