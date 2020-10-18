using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Data.EntityConfiguration
{
    public class ProjectCompanyShareConfiguration : EntityTypeConfiguration<ProjectCompanyShare>
    {
        public ProjectCompanyShareConfiguration()
        {
            ToTable("ProjectCompanyShares");
            HasKey(f => f.Id);
            Property(f => f.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(f => f.CompanyStatus).IsRequired().HasMaxLength(50);
            Property(f => f.ShareType).IsRequired();
            Property(f => f.SharePart).IsRequired();
            Property(f => f.ShareStartDate).IsRequired();

            HasRequired(x => x.OwnerProjectCompany)
                .WithMany(x => x.OwnerProjectCompanyShares)
                .HasForeignKey(x => x.OwnerProjectCompanyId)
                .WillCascadeOnDelete(false);

            HasRequired(x => x.DependentProjectCompany)
                .WithMany(x => x.DependentProjectCompanyShares)
                .HasForeignKey(x => x.DependentProjectCompanyId);
        }
    }
}