using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models.Registers
{
    public class Register5 : IRegister
    {
        public int Id { get; set; }
        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompany OwnerProjectCompany { get; set; }
        public Year Year { get; set; }
        public RegisterType Type { get; set; }
        public string Currency { get; set; }
        public double SumProfit { get; set; }
        public double SumPercentForBondsProfit { get; set; }
        public double PartPercentForBondsProfit { get; set; }

    }
}
