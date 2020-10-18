using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Data.EntityConfiguration.Register
{
	public class Register11Configuration : EntityTypeConfiguration<Register11>
	{
		public Register11Configuration()
		{
			ToTable("Registers11");
			HasKey(r => r.Id);
			Property(r => r.Year).IsRequired();
			Property(r => r.Type);
			Property(r => r.Currency).IsRequired();
			HasRequired(x => x.OwnerProjectCompany).WithMany(x => x.Registers11).HasForeignKey(x => x.OwnerProjectCompanyId);
		}
	}
}
