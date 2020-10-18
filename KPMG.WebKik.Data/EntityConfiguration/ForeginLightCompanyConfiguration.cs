using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Companies;

namespace KPMG.WebKik.Data.EntityConfiguration
{
    public class ForeginLightCompanyConfiguration : EntityTypeConfiguration<ForeignLightCompany>
    {
        public ForeginLightCompanyConfiguration()
        {
            ToTable("ForeginLightCompanies");
            HasKey(f => f.Id);
            //Property(f => f.Number).IsRequired().HasMaxLength(50);
            Property(f => f.EnglishName).IsRequired().HasMaxLength(255);
            Property(f => f.RussianName).IsRequired().HasMaxLength(255);
            Property(f => f.FoundDate).IsRequired();
            Property(f => f.RequisitesEng).HasMaxLength(500);
            Property(f => f.RequisitesRus).HasMaxLength(500);
            Property(f => f.RegNumber).HasMaxLength(50);
            Property(f => f.OtherInfo).HasMaxLength(500);
            HasRequired(x => x.ProjectCompany).WithOptional(x => x.ForeignLightCompany);
            HasRequired(x => x.CountryCode).WithMany(x => x.ForeignLightCompaies).HasForeignKey(x => x.CountryCodeId);
            HasRequired(f => f.ForeignOrganizationalFormCode).WithMany(f => f.ForeignLightCompanies).HasForeignKey(f => f.ForeignOrganizationalFormCodeId);
        }
    }
}