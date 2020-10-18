using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models.Companies;

namespace KPMG.WebKik.Data.EntityConfiguration
{
    public class IndividualCompanyConfiguration : EntityTypeConfiguration<IndividualCompany>
    {
        public IndividualCompanyConfiguration()
        {
            ToTable("IndividualCompanies");
            HasKey(f => f.Id);
            Property(f => f.INN).IsRequired();
            Property(f => f.Surname).IsRequired().HasMaxLength(255);
            Property(f => f.Name).IsRequired().HasMaxLength(255);
            Property(f => f.MiddleName).IsRequired().HasMaxLength(255);
            Property(f => f.BirthDate).IsRequired();

            HasRequired(x => x.ProjectCompany).WithOptional(x => x.IndividualCompany);
            HasOptional(i => i.GenderCode).WithMany(g => g.IndividualCompanies).HasForeignKey(i => i.GenderCodeId);
            HasRequired(i => i.VerifedPersonalityDocInfo).WithMany(d => d.VerifedPersonalityDocInfo).HasForeignKey(i => i.VerifedPersonalityDocInfoId).WillCascadeOnDelete(false);
            HasRequired(i => i.ConfirmedPersonalityDocInfo).WithMany(d => d.ConfirmedPersonalityDocInfo).HasForeignKey(i => i.ConfirmedPersonalityDocInfoId);
            HasRequired(i => i.RussianLocationCode).WithMany(r => r.IndividulaCompanies).HasForeignKey(i => i.RussianLocationCodeId);
            HasRequired(i => i.RegionCode).WithMany(r => r.IndividualCompanies).HasForeignKey(i => i.RegionCodeId);
            HasOptional(i => i.ForeignCountryCode).WithMany(r => r.IndividualCompanies).HasForeignKey(i => i.ForeignCountryCodeId);
            HasOptional(i => i.CitizenshipCode).WithMany().HasForeignKey(i => i.CitizenshipCodeId);
        }
    }
}