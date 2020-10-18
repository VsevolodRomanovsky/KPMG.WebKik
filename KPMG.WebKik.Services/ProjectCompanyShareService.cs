using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Contracts.Service;
using System.Collections.Generic;
using System.Threading.Tasks;
using KPMG.WebKik.Models.ProjectCompanies;
using System;
using KPMG.WebKik.Models.Directories;
using System.Linq;
using KPMG.WebKik.Models.Registers;
using KPMG.Webkik.Utils;
using KPMG.WebKik.Models;
using KPMG.WebKik.Contracts.Algorithms;

namespace KPMG.WebKik.Services
{
    public class ProjectCompanyShareService : EntityService<ProjectCompanyShare, int>, IProjectCompanyShareService
    {
        private readonly IEntityRepository<DoubleTaxationAgreementCountryCode, int> sidnRepository;
        private readonly IEntityRepository<EAECCountryCode, int> eaecRepository;
        private readonly IEntityRepository<ProjectCompany, int> companyRepository;
        private readonly IFactShareCalculation factShareCalculator;
        private readonly IKIKCompanyCalculation kikCalculator;
        private readonly IProjectCompanyService companyService;

        public ProjectCompanyShareService(IProjectCompanyShareRepository repository,
            IEntityRepository<DoubleTaxationAgreementCountryCode, int> sidnRepository,
            IEntityRepository<EAECCountryCode, int> eaecRepository,
            IEntityRepository<ProjectCompany, int> companyRepository,
            IFactShareCalculation factShareCalculator,
            IKIKCompanyCalculation kikCalculator,
            IProjectCompanyService companyService) : base(repository)
        {
            this.sidnRepository = sidnRepository;
            this.eaecRepository = eaecRepository;
            this.companyRepository = companyRepository;
            this.factShareCalculator = factShareCalculator;
            this.kikCalculator = kikCalculator;
            this.companyService = companyService;
        }

        public override async Task<ProjectCompanyShare> GetById(int id)
        {
            var result = await repository
                .Where(x => x.Id == id)
                .Include(c => c.OwnerProjectCompany)
                .Include(c => c.DependentProjectCompany)
                .SingleAsync();

            return result;
        }

        public async Task<IList<ProjectCompanyShare>> GetAllByProjectCompanyId(int companyId, DateTime? date = null)
        {
            var checkdate = date?.Date ?? DateTime.Today;

            var q = repository.Where(c => c.OwnerProjectCompanyId == companyId);

            if (date != null)
            {
                q = q
                    .Where(x => x.ShareStartDate <= checkdate)
                    .Where(x => x.ShareFinishDate >= checkdate || x.ShareFinishDate == null);
            }

            return await q.Include(x => x.OwnerProjectCompany, c => c.DependentProjectCompany).ToListAsync();
        }

        public async Task<IList<ProjectCompanyShare>> GetAllByProjectId(int projectId, DateTime? date = null)
        {
            var checkdate = date?.Date ?? DateTime.Today;

            var q = repository.Where(c => c.OwnerProjectCompany.ProjectId == projectId);

            if (date != null)
            {
                q = q
                    .Where(x => x.ShareStartDate <= checkdate)
                    .Where(x => x.ShareFinishDate >= checkdate || x.ShareFinishDate == null);
            }

            return await q.Include(x => x.OwnerProjectCompany, c => c.DependentProjectCompany).ToListAsync();
        }


        public async Task<IList<ProjectCompany>> GetAllKIKsByProjectCompanyId(int companyId)
        {
            var shares = await GetFactForKIKByProjectCompanyId(companyId);

            return shares.Where(share => kikCalculator.IsKIKCompany(share))
                .Select(share => share.DependentProjectCompany)
                .ToArray();
        }

        public async Task<IList<ProjectCompanyFactShare>> GetFactByProjectCompanyId(int companyId, DateTime? date = null)
        {
            var projectId = (await companyRepository.Where(x => x.Id == companyId).FirstOrDefaultAsync()).ProjectId;
            var checkdate = date?.Date ?? DateTime.Today;

            var shares = await repository.Where(c => c.OwnerProjectCompany.ProjectId == projectId)
                .Where(x => x.ShareStartDate <= checkdate)
                .Where(x => x.ShareFinishDate >= checkdate || x.ShareFinishDate == null)
                .Include(x => x.OwnerProjectCompany, c => c.DependentProjectCompany, t => t.OwnerProjectCompany.DomesticCompany).ToListAsync();

            return factShareCalculator.GetFactShares(shares)
                .Where(x => x.OwnerProjectCompanyId == companyId)
                .ToList();
        }

        public async Task<IList<ProjectCompanyFactShare>> GetFactForKIKByProjectCompanyId(int companyId, DateTime? date = null)
        {
            var projectId = (await companyRepository.Where(x => x.Id == companyId).FirstOrDefaultAsync()).ProjectId;
            var checkdate = date?.Date ?? DateTime.Today;

            var shares = await repository.Where(c => c.OwnerProjectCompany.ProjectId == projectId)
                .Where(x => x.ShareStartDate <= checkdate)
                .Where(x => x.ShareFinishDate >= checkdate || x.ShareFinishDate == null)
                .Where(x => !x.OwnerProjectCompany.DomesticCompany.IsPublic || x.OwnerProjectCompany.DomesticCompany == null)
                .Include(x => x.OwnerProjectCompany, c => c.DependentProjectCompany)
                .Include(x => x.OwnerProjectCompany.DomesticCompany).ToListAsync();

            return factShareCalculator.GetFactShares(shares)
                .Where(x => x.OwnerProjectCompanyId == companyId)
                .ToList();
        }

        public override async Task Update(ProjectCompanyShare entity)
        {
            var projectId = (await companyRepository.Where(x => x.OwnerProjectCompanyShares.Any(s => s.Id == entity.Id)).SingleOrDefaultAsync())?.ProjectId;
            await base.Update(entity);
            if (projectId.HasValue) await companyService.CalculateProjectInfo(projectId.Value);
        }

        public override async Task Delete(int id)
        {
            var projectId = (await companyRepository.Where(x => x.OwnerProjectCompanyShares.Any(s => s.Id == id)).SingleOrDefaultAsync())?.ProjectId;
            await base.Delete(id);
            if (projectId.HasValue) await companyService.CalculateProjectInfo(projectId.Value);
        }
    }
}
