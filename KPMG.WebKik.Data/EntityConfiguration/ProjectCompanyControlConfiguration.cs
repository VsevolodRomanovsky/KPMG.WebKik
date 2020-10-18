using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Data.EntityConfiguration
{
    public class ProjectCompanyControlConfiguration : EntityTypeConfiguration<ProjectCompanyControl>
    {
        public ProjectCompanyControlConfiguration()
        {
            ToTable("ProjectCompanyControls");
            HasKey(f => f.Id);
            Property(f => f.IsFounder).IsRequired();
            Property(f => f.IsControlledBy).IsRequired();
            Property(f => f.FoundDate).IsRequired();
            HasRequired(x => x.ProjectCompany).WithOptional(x => x.ProjectCompanyControl);
        }
    }
}