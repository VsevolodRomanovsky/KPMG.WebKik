using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Data.EntityConfiguration.Register
{
    public class Register2Configuration : EntityTypeConfiguration<Register2>
    {
        public Register2Configuration()
        {
            ToTable("Registers2");
            HasKey(r => r.Id);
            Property(r => r.Year).IsRequired();
            Property(r => r.Type);
            Property(r => r.Currency).IsRequired();
            Property(r => r.BalanceLoss).IsRequired();
            Property(r => r.BalanceLoss2012).IsRequired();
            Property(r => r.BalanceLoss2013).IsRequired();
            Property(r => r.BalanceLoss2014).IsRequired();
            Property(r => r.BalanceLoss00).IsRequired();
            Property(r => r.BalanceLoss01).IsRequired();
            Property(r => r.TaxBaseForTaxPeriod).IsRequired();
            Property(r => r.LossSumTaxBase).IsRequired();
            Property(r => r.SumLoss2012).IsRequired();
            Property(r => r.SumLoss2013).IsRequired();
            Property(r => r.SumLoss2014).IsRequired();
            Property(r => r.SumLoss00).IsRequired();
            Property(r => r.SumLoss01).IsRequired();
            Property(r => r.FullBalanceLoss).IsRequired();
            Property(r => r.FullBalanceLoss2012).IsRequired();
            Property(r => r.FullBalanceLoss2013).IsRequired();
            Property(r => r.FullBalanceLoss2014).IsRequired();
            Property(r => r.FullBalanceLoss00).IsRequired();
            Property(r => r.FullBalanceLoss01).IsRequired();

            HasRequired(x => x.OwnerProjectCompany).WithMany(x => x.Registers2).HasForeignKey(x => x.OwnerProjectCompanyId);
        }
    }
}
