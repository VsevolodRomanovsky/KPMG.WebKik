using KPMG.WebKik.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KPMG.WebKik.Data.EntityConfiguration
{
    public class NotificationOFParticipationConfiguration : EntityTypeConfiguration<NotificationOfParticipation>
    {
        public NotificationOFParticipationConfiguration()
        {
            ToTable("NotificationsOfParticipation");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CreatedDate).IsRequired();
            Property(x => x.Correction).IsRequired();
            Property(x => x.File).IsRequired();
            HasRequired(x => x.ProjectCompany).WithMany(x => x.Notifications).HasForeignKey(x => x.ProjectCompanyId);
            HasRequired(x => x.SubmissionGround).WithMany().HasForeignKey(x => x.SubmissionGroundId);
            HasRequired(x => x.Signatory).WithMany().HasForeignKey(x => x.SignatoryId).WillCascadeOnDelete(false);
        }
    }
}