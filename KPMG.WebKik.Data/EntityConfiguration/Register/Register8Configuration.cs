using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Data.EntityConfiguration.Register
{
	public class Register8Configuration : EntityTypeConfiguration<Register8>
	{
		public Register8Configuration()
		{
			ToTable("Registers8");
			HasKey(r => r.Id);
			Property(r => r.Year).IsRequired();
			Property(r => r.Type);
			Property(r => r.Currency).IsRequired();
			HasRequired(x => x.OwnerProjectCompany).WithMany(x => x.Registers8).HasForeignKey(x => x.OwnerProjectCompanyId);
		}
	}
}
