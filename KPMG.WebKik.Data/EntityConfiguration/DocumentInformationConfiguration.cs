using KPMG.WebKik.Models;
using System.Data.Entity.ModelConfiguration;

namespace KPMG.WebKik.Data.EntityConfiguration
{
    class DocumentInformationConfiguration : EntityTypeConfiguration<DocumentInformation>
    {
        public DocumentInformationConfiguration()
        {
            ToTable("DocumentInformations");
            HasKey(d => d.Id);
            Property(x => x.SeriesAndNumber).IsRequired();
            Property(x => x.IssueDate).IsRequired();
            Property(x => x.IssuePlace).IsRequired();
            HasRequired(d => d.DocumentCode).WithMany(d => d.DocumentInformationts).HasForeignKey(d => d.DocumentCodeId);
        }
    }
}
