using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models.Registers
{
    public class Register1 : IRegister, IEntity<int>
    {
        public int Id { get; set; }

        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompany OwnerProjectCompany { get; set; }
        public Year Year { get; set; }
        public RegisterType Type { get; set; }
        public string Currency { get; set; }

        public double ProfitAmountBeforeTax { get; set; }

        public double IncomeKIKNotIncluded { get; set; }
        public double IncomeFromRegisteredCapitals { get; set; }
        public double IncomeFromShares { get; set; }
        public double IncomeFromSecurities { get; set; }
        public double IncomeFromDerivatives { get; set; }
        public double IncomeFromSubsidiary { get; set; }
        public double IncomeFromReserveRestore { get; set; }

        public double CostsKIKNotIncluded { get; set; }
        public double CostsFromRegisteredCapitals { get; set; }
        public double CostsFromShares { get; set; }
        public double CostsFromSecurities { get; set; }
        public double CostsFromDerivatives { get; set; }
        public double CostsFromSubsidiary { get; set; }
        public double CostsFromReserveRestore { get; set; }
        public double IncomeAndCostsTotalAmount { get; set; }
        public double ProfitTotalAmountCorrection { get; set; }
        public double ReassessmentTotalAmount { get; set; }
        public double ReassessmentFromRegisteredCapitals { get; set; }
        public double ReassessmentFromShares { get; set; }
        public double ReassessmentFromSecurities { get; set; }
        public double ReassessmentFromDerivatives { get; set; }
        public double IncomeSymmetricCorrection { get; set; }
        public double IncomeNotIncludedInProfit { get; set; }
        public double CostsIncludedInProfit { get; set; }
        public double AdjustedProfitAmount { get; set; }
        public double ProfitExclusion  { get; set; }
        public double DividendsCurrentYear { get; set; }
        public double DistributedProfitAmount { get; set; }
        public double IncomeProperty { get; set; }
        public double LossProperty { get; set; }
        public double ProfitAmount { get; set; }
        public double AverageForeignCurrency { get; set; }
        public double ProfitAmountConvertedCurrency { get; set; }

        public double StandartKKIKProfit { get; set; }
        public double ReceivedDividends { get; set; }
        public double ProfitAmountCurrentYear{ get; set; }
        public double ProfitAmountForTax { get; set; }
        public double LossKIKFromPastYears { get; set; }
        public double CountableProfitAmountForTax { get; set; }
        public double ShareInKIKProfit { get; set; }
        public double PartKIKProfit { get; set; }
        public double ControlledProfitAmount { get; set; }
        public double KIKTaxBase { get; set; }

    }
}
