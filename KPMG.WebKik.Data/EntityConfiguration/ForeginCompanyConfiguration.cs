using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Companies;

namespace KPMG.WebKik.Data.EntityConfiguration
{
    public class ForeginCompanyConfiguration : EntityTypeConfiguration<ForeignCompany>
    {
        public ForeginCompanyConfiguration()
        {
            ToTable("ForeginCompanies");
            HasKey(f => f.Id);
            Property(f => f.Name).IsRequired().HasMaxLength(255);
            Property(f => f.FullName).IsRequired().HasMaxLength(500);
            //Property(f => f.Number).IsRequired();
            Property(f => f.RegistrationNumber).IsRequired();
            Property(f => f.Address).IsRequired().HasMaxLength(500);
            HasRequired(x => x.ProjectCompany).WithOptional(x => x.ForeignCompany);
            HasRequired(x => x.CountryCode).WithMany(x => x.ForeignCompanies).HasForeignKey(x => x.CountryCodeId);
            HasOptional(f => f.TaxPayerCode).WithMany(x => x.ForeignCompanies).HasForeignKey(x => x.TaxPayerCodeId).WillCascadeOnDelete(false);
        }
    }
}