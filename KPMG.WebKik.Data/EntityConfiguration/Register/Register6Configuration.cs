using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Data.EntityConfiguration.Register
{
    public class Register6Configuration : EntityTypeConfiguration<Register6>
    {
        public Register6Configuration()
        {
            ToTable("Registers6");
            HasKey(r => r.Id);
            Property(r => r.Year).IsRequired();
            Property(r => r.Type);
            Property(r => r.Currency).IsRequired();
            Property(r => r.IncomeTaxRated).IsRequired();
            Property(r => r.IncomeTaxDeducted).IsRequired();
            Property(r => r.IncomeTaxCorrection).IsRequired();
            Property(r => r.KIKProfit).IsRequired();
            Property(r => r.IncomeTaxEffected).IsRequired();
            Property(r => r.KIKDividends).IsRequired();
            Property(r => r.KIKProfitMinusDividends).IsRequired();
            Property(r => r.AverageTaxRate).IsRequired();
            Property(r => r.AverageTaxRatePart).IsRequired();

            HasRequired(x => x.OwnerProjectCompany).WithMany(x => x.Registers6).HasForeignKey(x => x.OwnerProjectCompanyId);
        }
    }
}
