using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models.Registers
{
    public class Register3 : IRegister
    {
        public int Id { get; set; }

        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompany OwnerProjectCompany { get; set; }
        public Year Year { get; set; }
        public RegisterType Type { get; set; }
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
        public double TaxSum{ get; set; }

        public double ForeginContryProfitCurrency{ get; set; }
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



    }
}
