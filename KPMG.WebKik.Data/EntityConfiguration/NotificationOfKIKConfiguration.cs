using KPMG.WebKik.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KPMG.WebKik.Data.EntityConfiguration
{
    public class NotificationOfKIKConfiguration : EntityTypeConfiguration<NotificationOfKIK>
    {
        public NotificationOfKIKConfiguration()
        {

            ToTable("NotificationOfKIKs");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CreatedDate).IsRequired();
            Property(x => x.Year).IsRequired();
            Property(x => x.Correction).IsRequired();
            Property(x => x.TaxAuthorityCode);
            Property(x => x.File).IsRequired();
            HasRequired(x => x.Signatory).WithMany().HasForeignKey(x => x.SignatoryId).WillCascadeOnDelete(false);
            HasRequired(x => x.ProjectCompany).WithMany(x => x.NotificationOfKIKs).HasForeignKey(x => x.ProjectCompanyId);

        }
    }
}
