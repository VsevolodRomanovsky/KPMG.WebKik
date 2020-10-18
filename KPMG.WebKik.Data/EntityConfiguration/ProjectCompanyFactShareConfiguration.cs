using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Data.EntityConfiguration
{
    //public class ProjectCompanyFactShareConfiguration : EntityTypeConfiguration<ProjectCompanyFactShare>
    //{
    //    public ProjectCompanyFactShareConfiguration()
    //    {
    //        ToTable("ProjectCompanyFactShares");
    //        HasKey(f => f.Id);
    //        Property(f => f.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

    //        HasRequired(x => x.OwnerProjectCompany)
    //            .WithMany(x => x.OwnerProjectCompanyFactShares)
    //            .HasForeignKey(x => x.OwnerProjectCompanyId)
    //            .WillCascadeOnDelete(false);

    //        HasRequired(x => x.DependentProjectCompany)
    //            .WithMany(x => x.DependentProjectCompanyFactShares)
    //            .HasForeignKey(x => x.DependentProjectCompanyId);
    //    }
    //}
}