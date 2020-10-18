using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models.Registers
{
    public class Register7 : IRegister
    {
        public int Id { get; set; }

        public Year Year { get; set; }
        public RegisterType Type { get; set; }
        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompany OwnerProjectCompany { get; set; }
        public string Currency { get; set; }
        public double KikTotalSumIncome { get; set; }
        public double SumIncomeSRP { get; set; }
        public double PartPercentSRPIncome { get; set; }
    }
}
