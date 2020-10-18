using System.Data.Entity;
using KPMG.WebKik.Contracts.Repository;
using ProjectCompany = KPMG.WebKik.Models.ProjectCompanies.ProjectCompany;
using System.Data.Entity.Migrations;

namespace KPMG.WebKik.Data
{
    public class ProjectCompanyRepository : EntityRepository<ProjectCompany, int>, IProjectCompanyRepository
    {
        public ProjectCompanyRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override void Update(ProjectCompany entity)
        {
            var entry = DbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                DbContext.Set<ProjectCompany>().Attach(entity);
                entry = DbContext.Entry(entity);
            }
            if (entity.DomesticCompany != null)
                DbContext.Entry(entity.DomesticCompany).State = EntityState.Modified;
            if (entity.ForeignCompany != null)
                DbContext.Entry(entity.ForeignCompany).State = EntityState.Modified;
            if (entity.ForeignLightCompany != null)
                DbContext.Entry(entity.ForeignLightCompany).State = EntityState.Modified;
            if (entity.IndividualCompany != null)
            {
                DbContext.Entry(entity.IndividualCompany).State = EntityState.Modified;
                DbContext.Entry(entity.IndividualCompany.ConfirmedPersonalityDocInfo).State = EntityState.Modified;
                DbContext.Entry(entity.IndividualCompany.VerifedPersonalityDocInfo).State = EntityState.Modified;
            }

            entry.State = EntityState.Modified;
        }
    }
}
