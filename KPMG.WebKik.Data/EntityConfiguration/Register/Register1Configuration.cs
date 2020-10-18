using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Data.EntityConfiguration.Register
{
    public class Register1Configuration : EntityTypeConfiguration<Register1>
    {
        public Register1Configuration()
        {
            ToTable("Registers1");
            HasKey(r => r.Id);
            Property(r => r.Year).IsRequired();
            Property(r => r.Type);
            Property(r => r.Currency).IsRequired();
            Property(r => r.ProfitAmountBeforeTax).IsRequired();
            Property(r => r.IncomeKIKNotIncluded).IsRequired();
            Property(r => r.IncomeFromRegisteredCapitals).IsRequired();
            Property(r => r.IncomeFromShares).IsRequired();
            Property(r => r.IncomeFromSecurities).IsRequired();
            Property(r => r.IncomeFromDerivatives).IsRequired();
            Property(r => r.IncomeFromSubsidiary).IsRequired();
            Property(r => r.IncomeFromReserveRestore).IsRequired();
            Property(r => r.CostsKIKNotIncluded).IsRequired();
            Property(r => r.CostsFromRegisteredCapitals).IsRequired();
            Property(r => r.CostsFromShares).IsRequired();
            Property(r => r.CostsFromSecurities).IsRequired();
            Property(r => r.CostsFromDerivatives).IsRequired();
            Property(r => r.CostsFromSubsidiary).IsRequired();
            Property(r => r.CostsFromReserveRestore).IsRequired();
            Property(r => r.IncomeAndCostsTotalAmount).IsRequired();
            Property(r => r.ProfitTotalAmountCorrection).IsRequired();
            Property(r => r.ReassessmentTotalAmount).IsRequired();
            Property(r => r.ReassessmentFromRegisteredCapitals).IsRequired();
            Property(r => r.ReassessmentFromShares).IsRequired();
            Property(r => r.ReassessmentFromSecurities).IsRequired();
            Property(r => r.ReassessmentFromDerivatives).IsRequired();
            Property(r => r.IncomeSymmetricCorrection).IsRequired();
            Property(r => r.IncomeNotIncludedInProfit).IsRequired();
            Property(r => r.CostsIncludedInProfit).IsRequired();
            Property(r => r.AdjustedProfitAmount).IsRequired();
            Property(r => r.ProfitExclusion).IsRequired();
            Property(r => r.DividendsCurrentYear).IsRequired();
            Property(r => r.DistributedProfitAmount).IsRequired();
            Property(r => r.IncomeProperty).IsRequired();
            Property(r => r.LossProperty).IsRequired();
            Property(r => r.ProfitAmount).IsRequired();
            Property(r => r.AverageForeignCurrency).IsRequired();
            Property(r => r.ProfitAmountConvertedCurrency).IsRequired();
            Property(r => r.StandartKKIKProfit).IsRequired();
            Property(r => r.ReceivedDividends).IsRequired();
            Property(r => r.ProfitAmountCurrentYear).IsRequired();
            Property(r => r.ProfitAmountForTax).IsRequired();
            Property(r => r.LossKIKFromPastYears).IsRequired();
            Property(r => r.CountableProfitAmountForTax).IsRequired();
            Property(r => r.ShareInKIKProfit).IsRequired();
            Property(r => r.PartKIKProfit).IsRequired();
            Property(r => r.ControlledProfitAmount).IsRequired();
            Property(r => r.KIKTaxBase).IsRequired();

            HasRequired(x => x.OwnerProjectCompany).WithMany(x => x.Registers1).HasForeignKey(x => x.OwnerProjectCompanyId);
        }
    }
}
