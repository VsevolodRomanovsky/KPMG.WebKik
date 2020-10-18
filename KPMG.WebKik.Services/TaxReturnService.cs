using System.Collections.Generic;
using System.Threading.Tasks;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Contracts.Repository;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Contracts.Algorithms;
using System;
using KPMG.WebKik.DocumentProcessing.TaxReturn;
using KPMG.WebKik.Contracts.Service.Registers;

namespace KPMG.WebKik.Services
{
    public class TaxReturnService : EntityService<TaxReturn, int>, ITaxReturnService
    {
        private readonly IEntityRepository<ProjectCompanyShare, int> shareRepository;
        private readonly IEntityRepository<ProjectCompany, int> companyRepository;
        private readonly IFactShareCalculation factShareCalculation;
        private readonly ISignatoryService signatureService;
        private readonly IProjectCompanyShareService shareService;
        private IProjectCompanyService projectCompanyService;
        private IRegister1Service register1Service;
        private IRegister3Service register3Service;
        private IRegister9Service register9Service;

        public TaxReturnService(
            IEntityRepository<TaxReturn, int> repository,
            IEntityRepository<ProjectCompany, int> companyRepository,
            IEntityRepository<ProjectCompanyShare, int> shareRepository,
            IProjectCompanyShareService shareService,
            IFactShareCalculation factShareCalculation,
            ISignatoryService signatureService,
            IProjectCompanyService projectCompanyService,
            IRegister1Service register1Service,
            IRegister3Service register3Service,
            IRegister9Service register9Service) : base(repository)
        {
            this.shareRepository = shareRepository;
            this.companyRepository = companyRepository;
            this.factShareCalculation = factShareCalculation;
            this.signatureService = signatureService;
            this.shareService = shareService;
            this.projectCompanyService = projectCompanyService;
            this.register1Service = register1Service;
            this.register3Service = register3Service;
            this.register9Service = register9Service;
        }

        public async Task<IEnumerable<TaxReturn>> GetByProjectId(int projectId)
        {
            return await repository
                .Where(x => x.ProjectCompany.Project.Id == projectId)
                .Include(
                    x => x.ProjectCompany)
                .ToListAsync();
        }

        public async Task<byte[]> GetDocument(int companyId, string path, int year)
        {
            var company = await projectCompanyService.GetById(companyId);
            //var signature = await signatureService.GetById(sigantoryId);
            var factShares = await shareService.GetFactByProjectCompanyId(companyId, DateTime.Now);

            return await new TaxReturnWorkbook(projectCompanyService, shareService, register1Service, register3Service, register9Service)
                .GetDocumentData(company, factShares, path, year);
        }
    }
}
