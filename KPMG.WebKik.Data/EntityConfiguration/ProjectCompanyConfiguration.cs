using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Data.EntityConfiguration
{
    public class ProjectCompanyConfiguration : EntityTypeConfiguration<ProjectCompany>
    {
        public ProjectCompanyConfiguration()
        {
            ToTable("ProjectCompanies");
            HasKey(f => f.Id);
            Property(f => f.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(f => f.ModifiedDate).IsRequired();
            Property(f => f.IsControlCompany);
            Property(f => f.IsKIKCompany);
            HasRequired(x => x.Project).WithMany(x => x.ProjectCompanies).HasForeignKey(x => x.ProjectId);
        }
    }
}