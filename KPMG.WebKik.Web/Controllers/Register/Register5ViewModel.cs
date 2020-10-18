using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;
using KPMG.WebKik.Web.Controllers.ProjectCompanyShare;

namespace KPMG.WebKik.Web.Controllers.Register
{
    public class Register5ViewModel: IEntity<int>
    {
        public int Id { get; set; }
        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompanyViewModel OwnerProjectCompany { get; set; }
        public RegisterType Type { get; set; }
        public Year Year { get; set; }
        public string Currency { get; set; }
        public double SumProfit { get; set; }
        public double SumPercentForBondsProfit { get; set; }
        public double PartPercentForBondsProfit { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Register5, Register5ViewModel>();
            cfg.CreateMap<Register5ViewModel, Register5>()
                .ForMember(r => r.OwnerProjectCompany, c => c.Ignore());
        }
    }
}