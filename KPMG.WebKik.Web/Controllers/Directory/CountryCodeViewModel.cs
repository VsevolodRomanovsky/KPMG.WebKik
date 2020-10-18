using AutoMapper.Configuration;
using KPMG.WebKik.Models.Directories;
using KPMG.WebKik.Web.Automapper;

namespace KPMG.WebKik.Web.Controllers.Directory
{
    public class CountryCodeViewModel : IDirectoryEntry
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string FullName { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<CountryCode, CountryCodeViewModel>();
            cfg.CreateMap<CountryCodeViewModel, CountryCode>()
                .ForMember(x => x.ForeignLightCompaies, y => y.Ignore())
                .ForMember(x => x.IndividualCompanies, y => y.Ignore())
                .ForMember(x => x.ForeignCompanies, y => y.Ignore());
        }
    }
}