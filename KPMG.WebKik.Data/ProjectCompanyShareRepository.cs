using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models.ProjectCompanies;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPMG.WebKik.Data
{
    public class ProjectCompanyShareRepository : EntityRepository<ProjectCompanyShare, int>, IProjectCompanyShareRepository
    {
        public ProjectCompanyShareRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override void Update(ProjectCompanyShare entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            /*
            if (entity.TaxExemptions == null || entity.TaxExemptions.Count == 0)
            {
                var entry = DbContext.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    DbContext.Set<ProjectCompanyShare>().Attach(entity);
                    entry = DbContext.Entry(entity);
                }

                entry.State = EntityState.Modified;
            }
            else
            {
                var allExemptions = DbContext.Set<ProjectCompanyShare>()
                                .Where(x => x.Id == entity.Id)
                                .Include(x => x.TaxExemptions)
                                .FirstOrDefault();

                var addedExemptions = entity.TaxExemptions.Where(x => x.Id == 0).ToList();
                addedExemptions.ForEach(x => allExemptions.TaxExemptions.Add(x));

                var removedExemptions = allExemptions.TaxExemptions
                                    .Where(x => !entity.TaxExemptions.Select(c => c.Id).Contains(x.Id))
                                    .ToList();
                removedExemptions.ForEach(x => allExemptions.TaxExemptions.Remove(x));

                var removed = DbContext.Set<TaxExemption>().Where(x => x.ProjectCompanyShareId == entity.Id)
                        .ToList()
                        .Where(x => removedExemptions.Select(c => c.Id).Contains(x.Id))
                        .ToList();

                removed.ForEach(x => DbContext.Set<TaxExemption>().Remove(x));
            }
            */
        }
    }
}
