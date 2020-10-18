using System.Data.Entity;
using KPMG.WebKik.Data.EntityConfiguration;
using KPMG.WebKik.Data.Migrations;
using KPMG.WebKik.Models;
using KPMG.WebKik.Data.EntityConfiguration.Register;
using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Models.Directories;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Data
{
    public class WebKikDataContext : DbContext
    {
        public WebKikDataContext() : this("WebKikConnection")
        {
        }

        public WebKikDataContext(string connectionName) : base(connectionName)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.CommandTimeout = 600;
        }

        public static void Initialize(string connectionName)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WebKikDataContext, Configuration>());
            using (var context = new WebKikDataContext(connectionName))
            {
                context.Database.CommandTimeout = 180;
                context.Database.Initialize(true);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new ProjectConfiguration());
            modelBuilder.Configurations.Add(new ProjectCompanyConfiguration());
            modelBuilder.Configurations.Add(new DomesticCompanyConfiguration());
            modelBuilder.Configurations.Add(new ForeginCompanyConfiguration());
            modelBuilder.Configurations.Add(new ForeginLightCompanyConfiguration());
            modelBuilder.Configurations.Add(new IndividualCompanyConfiguration());
            modelBuilder.Configurations.Add(new ProjectCompanyShareConfiguration());
            //modelBuilder.Configurations.Add(new ProjectCompanyFactShareConfiguration());
            modelBuilder.Configurations.Add(new ProjectCompanyControlConfiguration());
            modelBuilder.Configurations.Add(new DocumentInformationConfiguration());
            modelBuilder.Configurations.Add(new SignatoryConfiguration());
            modelBuilder.Configurations.Add(new NotificationOFParticipationConfiguration());
            modelBuilder.Configurations.Add(new TaxReturnConfiguration());
            modelBuilder.Configurations.Add(new NotificationOfKIKConfiguration());
            modelBuilder.Configurations.Add(new Register1Configuration());
            modelBuilder.Configurations.Add(new Register2Configuration());
            modelBuilder.Configurations.Add(new Register3Configuration());
            modelBuilder.Configurations.Add(new Register4Configuration());
            modelBuilder.Configurations.Add(new Register5Configuration());
            modelBuilder.Configurations.Add(new Register6Configuration());
            modelBuilder.Configurations.Add(new Register7Configuration());
			modelBuilder.Configurations.Add(new Register8Configuration());
			modelBuilder.Configurations.Add(new Register8DataConfiguration());
			modelBuilder.Configurations.Add(new Register9Configuration());
			modelBuilder.Configurations.Add(new Register9DataConfiguration());
            modelBuilder.Configurations.Add(new Register10Configuration());
            modelBuilder.Configurations.Add(new Register10DataConfiguration());
			modelBuilder.Configurations.Add(new Register11Configuration());
			modelBuilder.Configurations.Add(new Register11DataConfiguration());
			modelBuilder.Configurations.Add(new SupportingDocumentsConfiguration());
			base.OnModelCreating(modelBuilder);
        }

        public IDbSet<Project> Projects { get; set; }
        public IDbSet<ProjectCompany> ProjectCompanies { get; set; }
        public IDbSet<ProjectCompanyShare> ProjectCompanyShares { get; set; }
        public IDbSet<User> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<TaxPayerCode> TaxPayerCodes { get; set; }
        public IDbSet<NotificationSubmissionGround> NotificationSubmissionGrounds { get; set; }
        public IDbSet<SignatoryCode> SignatoryCodes { get; set; }
        public IDbSet<CountryCode> CountryCodes { get; set; }
        public IDbSet<ForeignOrganizationalFormCode> ForeignOrganizationalFormCodes { get; set; }
        public IDbSet<ForeignStructuresFoundersCode> ForeignStructuresFoundersCodes { get; set; }
        public IDbSet<ForeignStructurePersonControlCode> ForeignStructurePersonControlCodes { get; set; }
        public IDbSet<IncomeEntitledPersonCode> IncomeEntitledPersonCodes { get; set; }
        public IDbSet<DocumentCode> DocumentCodes { get; set; }
        public IDbSet<RegionCode> RegionCodes { get; set; }
        public IDbSet<GenderCode> GenderCodes { get; set; }
        public IDbSet<CitizenshipCode> CitizenshipCodes { get; set; }
        public IDbSet<RussianLocationCode> RussianLocationCodes { get; set; }
        public IDbSet<EAECCountryCode> EAECCountryCodes { get; set; }
        public IDbSet<DoubleTaxationAgreementCountryCode> DoubleTaxationAgreementCountryCodes { get; set; }
        public IDbSet<DocumentInformation> DocumentInformations { get; set; }
        public IDbSet<Register1> Registers1 { get; set; }
        public IDbSet<Register2> Registers2 { get; set; }
        public IDbSet<Register3> Registers3 { get; set; }
        public IDbSet<Register4> Registers4 { get; set; }
        public IDbSet<Register5> Registers5 { get; set; }
        public IDbSet<Register6> Registers6 { get; set; }
        public IDbSet<Register7> Registers7 { get; set; }

		public IDbSet<Register8> Registers8 { get; set; }

        public IDbSet<Register10> Registers10 { get; set; }

		public IDbSet<Register9> Registers9 { get; set; }

		public IDbSet<Register9Data> Registers9Data { get; set; }

        public IDbSet<Register10Data> Registers10Data { get; set; }

        public IDbSet<Register8Data> Registers8Data { get; set; }


		public IDbSet<Register11> Registers11 { get; set; }

		public IDbSet<Register11Data> Registers11Data { get; set; }

		public IDbSet<TaxExemption> TaxExemptions { get; set; }

        public IDbSet<ForeignLightCompany> ForeignLightCompany { get; set; }
        public IDbSet<ForeignCompany> ForeignCompany { get; set; }
        public IDbSet<IndividualCompany> IndividualCompany { get; set; }

        public IDbSet<DomesticCompany> DomesticCompany { get; set; }
		
		public IDbSet<SupportingDocument> SupportingDocuments { get; set; }
        public IDbSet<NotificationOfKIK> NotificationOfKIK { get; set; }
        public IDbSet<TaxReturn> TaxReturn { get; set; }

    }
}
