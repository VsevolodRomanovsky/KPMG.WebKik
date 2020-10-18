using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models.Registers
{
    public class Register6 : IRegister
    {
        public const double ProfitTaxRateArticle1 = 0.2;
        public const double ProfitTaxRateArticel3 = 0.13;

        public int Id { get; set; }
        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompany OwnerProjectCompany { get; set; }
        public Year Year { get; set; }
        public RegisterType Type { get; set; }
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
     

    }
}
