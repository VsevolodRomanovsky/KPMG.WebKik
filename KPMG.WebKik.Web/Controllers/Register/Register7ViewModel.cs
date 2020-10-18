using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;
using KPMG.WebKik.Web.Controllers.ProjectCompanyShare;

namespace KPMG.WebKik.Web.Controllers.Register
{
    public class Register7ViewModel: IEntity<int>
    {
        public int Id { get; set; }
        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompanyViewModel OwnerProjectCompany { get; set; }
        public RegisterType Type { get; set; }
        public Year Year { get; set; }
        public string Currency { get; set; }
        public double KikTotalSumIncome { get; set; }
        public double SumIncomeSRP { get; set; }
        public double PartPercentSRPIncome { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Register7, Register7ViewModel>();
            cfg.CreateMap<Register7ViewModel, Register7>()
                 .ForMember(r => r.OwnerProjectCompany, c => c.Ignore());
        }
    }
}