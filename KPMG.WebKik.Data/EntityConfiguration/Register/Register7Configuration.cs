using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Data.EntityConfiguration.Register
{
    public class Register7Configuration : EntityTypeConfiguration<Register7>
    {
        public Register7Configuration()
        {
            ToTable("Registers7");
            HasKey(r => r.Id);
            Property(r => r.Year).IsRequired();
            Property(r => r.Type);
            Property(r => r.Currency).IsRequired();
            Property(r => r.KikTotalSumIncome).IsRequired();
            Property(r => r.SumIncomeSRP).IsRequired();
            Property(r => r.PartPercentSRPIncome).IsRequired();
            HasRequired(x => x.OwnerProjectCompany).WithMany(x => x.Registers7).HasForeignKey(x => x.OwnerProjectCompanyId);
        }
    }
}
