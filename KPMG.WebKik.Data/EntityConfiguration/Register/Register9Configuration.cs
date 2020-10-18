using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Data.EntityConfiguration.Register
{
	public class Register9Configuration : EntityTypeConfiguration<Register9>
	{
		public Register9Configuration()
		{
			ToTable("Registers9");
			HasKey(r => r.Id);
			Property(r => r.Year).IsRequired();
			Property(r => r.Type);
			Property(r => r.Currency).IsRequired();
			HasRequired(x => x.OwnerProjectCompany).WithMany(x => x.Registers9).HasForeignKey(x => x.OwnerProjectCompanyId);
		}
	}
}
