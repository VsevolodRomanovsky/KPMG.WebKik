using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;
using KPMG.WebKik.Web.Controllers.ProjectCompanyShare;

namespace KPMG.WebKik.Web.Controllers.Register
{
    public class Register3ViewModel: IEntity<int>
    {
        public int Id { get; set; }
        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompanyViewModel OwnerProjectCompany { get; set; }
        public RegisterType Type { get; set; }
        public Year Year { get; set; }
        public string Currency { get; set; }

        public double KIKProfitCurrency { get; set; }
        public double KIKConversionRate { get; set; }
        public double KIKProfitRUR { get; set; }
        public double KIKSharePart { get; set; }
        public double KIKProfitSharePart { get; set; }
        public double KIKProfitPartForTax { get; set; }
        public double KIKTaxBasePart { get; set; }
        public double TaxPercentValue { get; set; }
        public double TaxSumCurrency { get; set; }
        public double TaxSum { get; set; }

        public double ForeginContryProfitCurrency { get; set; }
        public double ForeginContryProfitRUR { get; set; }
        public double ForeginContryEarningsCurrency { get; set; }
        public double ForeginContryEarningsRUR { get; set; }

        public double DomesticProfitCurrency { get; set; }
        public double DomesticContryProfitRUR { get; set; }
        public double DomesticContryEarningsCurrency { get; set; }
        public double DomesticContryEarningsRUR { get; set; }

        public double RURResultSum { get; set; }
        public double RURResultForPart { get; set; }
        public double RURResultTax { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Register3, Register3ViewModel>();
            cfg.CreateMap<Register3ViewModel, Register3>()
                .ForMember(r => r.OwnerProjectCompany, c => c.Ignore());
        }
    }
}