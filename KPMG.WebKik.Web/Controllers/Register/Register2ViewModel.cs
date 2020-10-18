using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;
using KPMG.WebKik.Web.Controllers.ProjectCompanyShare;

namespace KPMG.WebKik.Web.Controllers.Register
{
    public class Register2ViewModel : IEntity<int>
    {
        public int Id { get; set; }
        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompanyViewModel OwnerProjectCompany { get; set; }
        public RegisterType Type { get; set; }
        public Year Year { get; set; }
        public string Currency { get; set; }

        public double BalanceLoss { get; set; }
        public double BalanceLoss2012 { get; set; }
        public double BalanceLoss2013 { get; set; }
        public double BalanceLoss2014 { get; set; }
        public double BalanceLoss00 { get; set; }
        public double BalanceLoss01 { get; set; }
        public double TaxBaseForTaxPeriod { get; set; }
        public double LossSumTaxBase { get; set; }
        public double SumLoss2012 { get; set; }
        public double SumLoss2013 { get; set; }
        public double SumLoss2014 { get; set; }
        public double SumLoss00 { get; set; }
        public double SumLoss01 { get; set; }
        public double FullBalanceLoss { get; set; }
        public double FullBalanceLoss2012 { get; set; }
        public double FullBalanceLoss2013 { get; set; }
        public double FullBalanceLoss2014 { get; set; }
        public double FullBalanceLoss00 { get; set; }
        public double FullBalanceLoss01 { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Register2, Register2ViewModel>();
            cfg.CreateMap<Register2ViewModel, Register2>()
                .ForMember(r => r.OwnerProjectCompany, c => c.Ignore());
        }
    }
}