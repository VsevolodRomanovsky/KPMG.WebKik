using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models;

namespace KPMG.WebKik.Data.EntityConfiguration
{
    public class SignatoryConfiguration : EntityTypeConfiguration<Signatory>
    {
        public SignatoryConfiguration()
        {
            ToTable("Signatories");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.LastName).IsRequired().HasMaxLength(255);
            Property(x => x.FirstName).IsRequired().HasMaxLength(255);
            Property(x => x.MiddleName).IsRequired().HasMaxLength(255);
            HasRequired(x => x.SignatoryCode).WithMany(x => x.Signatories).HasForeignKey(x => x.SignatoryCodeId);
            HasRequired(x => x.ConfirmationDocument).WithMany(x => x.Signatories).HasForeignKey(x => x.ConfirmationDocumentId);
            HasRequired(x => x.ProjectCompany).WithMany(x => x.Signatories).HasForeignKey(x => x.ProjectCompanyId);
        }
    }
}