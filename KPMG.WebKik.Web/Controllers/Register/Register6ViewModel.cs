using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;
using KPMG.WebKik.Web.Controllers.ProjectCompanyShare;

namespace KPMG.WebKik.Web.Controllers.Register
{
    public class Register6ViewModel: IEntity<int>
    {
        public int Id { get; set; }
        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompanyViewModel OwnerProjectCompany { get; set; }
        public RegisterType Type { get; set; }
        public Year Year { get; set; }
        public string Currency { get; set; }

        public double IncomeTaxRated { get; set; }
        public double IncomeTaxDeducted { get; set; }
        public double IncomeTaxCorrection { get; set; }
        public double KIKProfit { get; set; }
        public double IncomeTaxEffected { get; set; }
        public double KIKDividends { get; set; }
        public double KIKProfitMinusDividends { get; set; }
        public double AverageTaxRate { get; set; }
        public double AverageTaxRatePart { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Register6, Register6ViewModel>();
            cfg.CreateMap<Register6ViewModel, Register6>()
                 .ForMember(r => r.OwnerProjectCompany, c => c.Ignore());
        }
    }
}