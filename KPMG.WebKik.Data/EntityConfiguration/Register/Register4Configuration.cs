using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Data.EntityConfiguration.Register
{
    public class Register4Configuration : EntityTypeConfiguration<Register4>
    {
        public Register4Configuration()
        {
            ToTable("Registers4");
            HasKey(r => r.Id);
            Property(r => r.Year).IsRequired();
            Property(r => r.Type);
            Property(r => r.Currency).IsRequired();

            Property(r => r.IncomeKIKTotalAmount).IsRequired();
            Property(r => r.IncomeNotIncluded).IsRequired();
            Property(r => r.IncomeFromRateDifference).IsRequired();
            Property(r => r.IncomeFromRegisteredCapitals).IsRequired();
            Property(r => r.IncomeFromShares).IsRequired();
            Property(r => r.IncomeFromSecurities).IsRequired();
            Property(r => r.IncomeFromDerivatives).IsRequired();
            Property(r => r.IncomeFromSubsidiary).IsRequired();
            Property(r => r.IncomeFromReserveRestore).IsRequired();
            Property(r => r.IncomeKIKSummary).IsRequired();
            Property(r => r.PassiveIncomeKIKSummary).IsRequired();
            Property(r => r.DividendsSum).IsRequired();
            Property(r => r.DividendsFromActiveCompanies).IsRequired();
            Property(r => r.DividendsFromHoldingCompanies).IsRequired();
            Property(r => r.IncomeFromAppropriationProfit).IsRequired();
            Property(r => r.IncomeFromDebentures).IsRequired();
            Property(r => r.IntellectialPropRightsIncome).IsRequired();
            Property(r => r.SharedPartsIncome).IsRequired();
            Property(r => r.FISSIncome).IsRequired();
            Property(r => r.ImmovablePropertyIncome).IsRequired();
            Property(r => r.LeasePropertyIncome).IsRequired();
            Property(r => r.InvestmentUnitsIncome).IsRequired();
            Property(r => r.ServicesIncome).IsRequired();
            Property(r => r.ServicesConsultingIncome).IsRequired();
            Property(r => r.ServicesLegalIncome).IsRequired();
            Property(r => r.ServicesAccountingIncome).IsRequired();
            Property(r => r.ServicesAuditIncome).IsRequired();
            Property(r => r.ServicesEngineeringIncome).IsRequired();
            Property(r => r.ServicesAdvertisingIncome).IsRequired();
            Property(r => r.ServicesMarketingIncome).IsRequired();
            Property(r => r.ServicesInformationProcessingIncome).IsRequired();
            Property(r => r.ServicesReseachAndDevelopmentIncome).IsRequired();
            Property(r => r.ServicesStaffProvidingIncome).IsRequired();
            Property(r => r.OtherIncome).IsRequired();
            Property(r => r.IncomeSumExceptDividendsFromActiveCompanies).IsRequired();
            Property(r => r.IncomeSumExceptDividendsFromHoldingCompanies).IsRequired();
            Property(r => r.PassivePartIncomeValue).IsRequired();
            Property(r => r.PassivePartWithoutDividendsIncomeValue).IsRequired();
            Property(r => r.PassivePartWithoutDividendsAndHoldingsIncomeValue).IsRequired();

            HasRequired(x => x.OwnerProjectCompany).WithMany(x => x.Registers4).HasForeignKey(x => x.OwnerProjectCompanyId);
        }
    }
}
