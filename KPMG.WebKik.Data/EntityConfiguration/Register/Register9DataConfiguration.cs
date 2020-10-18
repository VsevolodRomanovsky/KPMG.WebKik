using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Data.EntityConfiguration.Register
{
	public class Register9DataConfiguration : EntityTypeConfiguration<Register9Data>
	{
		public Register9DataConfiguration()
		{
			ToTable("Register9Data");
			HasKey(r => r.Id);
			HasRequired(x => x.Register9).WithMany(x => x.Register9Data).HasForeignKey(x => x.Register9Id);
		}
	}
}
