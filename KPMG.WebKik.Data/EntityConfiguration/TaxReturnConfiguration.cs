using KPMG.WebKik.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KPMG.WebKik.Data.EntityConfiguration
{
    public class TaxReturnConfiguration : EntityTypeConfiguration<TaxReturn>
    {
        public TaxReturnConfiguration()
        {

            ToTable("TaxReturns");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CreatedDate).IsRequired();
            Property(x => x.Year).IsRequired();
            Property(x => x.Correction).IsRequired();
            Property(x => x.File).IsRequired();
            HasRequired(x => x.Signatory).WithMany().HasForeignKey(x => x.SignatoryId).WillCascadeOnDelete(false);
            HasRequired(x => x.ProjectCompany).WithMany(x => x.TaxReturns).HasForeignKey(x => x.ProjectCompanyId);
            
        }
    }
}
