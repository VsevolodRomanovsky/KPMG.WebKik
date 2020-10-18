using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Models;

namespace KPMG.WebKik.Data.EntityConfiguration
{
    public class SupportingDocumentsConfiguration : EntityTypeConfiguration<SupportingDocument>
    {
        public SupportingDocumentsConfiguration()
        {
            ToTable("SupportingDocuments");
            //Property(x => x.Number).IsRequired();
            Property(x => x.CompanyType).IsRequired();
            Property(x => x.IsND).IsRequired();
            Property(x => x.IsUKIK).IsRequired();
            Property(x => x.IsUU).IsRequired();
			HasRequired(x => x.ProjectCompany).WithMany(x => x.SupportingDocuments).HasForeignKey(x => x.ProjectCompanyId);
			//	HasOptional()
			//   HasRequired(x => x.ProjectCompany).WithOptional(x => x.DomesticCompany);
		}
    }
}