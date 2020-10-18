using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Companies;

namespace KPMG.WebKik.Data.EntityConfiguration
{
    public class DomesticCompanyConfiguration : EntityTypeConfiguration<DomesticCompany>
    {
        public DomesticCompanyConfiguration()
        {
            ToTable("DomesticCompanies");
            HasKey(x => x.Id);
            //Property(x => x.Number).IsRequired();
            Property(x => x.FullName).IsRequired().HasMaxLength(255);
            Property(x => x.OGRN).IsRequired();
            Property(x => x.INN).IsRequired();
            Property(x => x.KPP).IsRequired();
            HasRequired(x => x.ProjectCompany).WithOptional(x => x.DomesticCompany);
        }
    }
}