using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Data.EntityConfiguration.Register
{
	public class Register8DataConfiguration : EntityTypeConfiguration<Register8Data>
	{
		public Register8DataConfiguration()
		{
			ToTable("Register8Data");
			HasKey(r => r.Id);
			HasRequired(x => x.Register8).WithMany(x => x.Register8Data).HasForeignKey(x => x.Register8Id);
		}
	}
}
