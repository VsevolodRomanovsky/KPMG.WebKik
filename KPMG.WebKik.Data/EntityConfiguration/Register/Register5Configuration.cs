using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Data.EntityConfiguration.Register
{
    public class Register5Configuration : EntityTypeConfiguration<Register5>
    {
        public Register5Configuration()
        {
            ToTable("Registers5");
            HasKey(r => r.Id);
            Property(r => r.Year).IsRequired();
            Property(r => r.Type);
            Property(r => r.Currency).IsRequired();
            Property(r => r.SumProfit).IsRequired();
            Property(r => r.SumPercentForBondsProfit).IsRequired();
            Property(r => r.PartPercentForBondsProfit).IsRequired();

            HasRequired(x => x.OwnerProjectCompany).WithMany(x => x.Registers5).HasForeignKey(x => x.OwnerProjectCompanyId);
        }
    }
}
