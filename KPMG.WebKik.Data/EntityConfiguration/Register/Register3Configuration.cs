using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Data.EntityConfiguration.Register
{
    public class Register3Configuration : EntityTypeConfiguration<Register3>
    {
        public Register3Configuration()
        {
            ToTable("Registers3");
            HasKey(r => r.Id);
            Property(r => r.Year).IsRequired();
            Property(r => r.Type);
            Property(r => r.Currency).IsRequired();

            Property(r => r.KIKProfitCurrency).IsRequired();
            Property(r => r.KIKProfitRUR).IsRequired();
            Property(r => r.KIKSharePart).IsRequired();
            Property(r => r.KIKTaxBasePart).IsRequired();
            Property(r => r.KIKProfitSharePart).IsRequired();
            Property(r => r.KIKConversionRate).IsRequired();
            Property(r => r.KIKProfitPartForTax).IsRequired();
            Property(r => r.TaxPercentValue).IsRequired();
            Property(r => r.TaxSumCurrency).IsRequired();
            Property(r => r.TaxSum).IsRequired();

            Property(r => r.ForeginContryProfitCurrency).IsRequired();
            Property(r => r.ForeginContryEarningsRUR).IsRequired();
            Property(r => r.ForeginContryEarningsCurrency).IsRequired();
            Property(r => r.ForeginContryEarningsRUR).IsRequired();
            Property(r => r.DomesticProfitCurrency).IsRequired();
            Property(r => r.DomesticContryProfitRUR).IsRequired();
            Property(r => r.DomesticContryEarningsCurrency).IsRequired();
            Property(r => r.DomesticContryEarningsRUR).IsRequired();
            Property(r => r.RURResultSum).IsRequired();
            Property(r => r.RURResultForPart).IsRequired();
            Property(r => r.RURResultTax).IsRequired();

            HasRequired(x => x.OwnerProjectCompany).WithMany(x => x.Registers3).HasForeignKey(x => x.OwnerProjectCompanyId);
        }
    }
}
