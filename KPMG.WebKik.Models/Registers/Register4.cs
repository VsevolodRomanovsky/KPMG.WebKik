using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models.Registers
{
    public class Register4 : IRegister
    {
        public int Id { get; set; }

        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompany OwnerProjectCompany { get; set; }
        public Year Year { get; set; }
        public RegisterType Type { get; set; }
        public string Currency { get; set; }

        public double IncomeKIKTotalAmount { get; set; }
        public double IncomeNotIncluded { get; set; }
        public double IncomeFromRateDifference { get; set; }
        public double IncomeFromRegisteredCapitals { get; set; }
        public double IncomeFromShares { get; set; }
        public double IncomeFromSecurities { get; set; }
        public double IncomeFromDerivatives { get; set; }
        public double IncomeFromSubsidiary { get; set; }
        public double IncomeFromReserveRestore { get; set; }

        public double IncomeKIKSummary { get; set; }
        public double PassiveIncomeKIKSummary { get; set; }
        public double DividendsSum { get; set; }
        public double DividendsFromActiveCompanies { get; set; }
        public double DividendsFromHoldingCompanies { get; set; }
        public double IncomeFromAppropriationProfit { get; set; }
        public double IncomeFromDebentures { get; set; }
        public double IntellectialPropRightsIncome { get; set; }
        public double SharedPartsIncome { get; set; }
        public double FISSIncome { get; set; }
        public double ImmovablePropertyIncome { get; set; }
        public double LeasePropertyIncome { get; set; }
        public double InvestmentUnitsIncome { get; set; }

        public double ServicesIncome { get; set; }
        public double ServicesConsultingIncome { get; set; }
        public double ServicesLegalIncome { get; set; }
        public double ServicesAccountingIncome { get; set; }
        public double ServicesAuditIncome { get; set; }
        public double ServicesEngineeringIncome { get; set; }
        public double ServicesAdvertisingIncome { get; set; }
        public double ServicesMarketingIncome { get; set; }
        public double ServicesInformationProcessingIncome { get; set; }
        public double ServicesReseachAndDevelopmentIncome { get; set; }
        public double ServicesStaffProvidingIncome { get; set; }
        public double OtherIncome { get; set; }
        public double IncomeSumExceptDividendsFromActiveCompanies { get; set; }
        public double IncomeSumExceptDividendsFromHoldingCompanies { get; set; }
        public double PassivePartIncomeValue { get; set; }
        public double PassivePartWithoutDividendsIncomeValue { get; set; }
        public double PassivePartWithoutDividendsAndHoldingsIncomeValue { get; set; }


    }
}
