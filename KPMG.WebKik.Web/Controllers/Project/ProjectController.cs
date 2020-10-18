using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Contracts.Algorithms;
using System;
using System.Linq;
using WebGrease.Css.Ast.Selectors;

namespace KPMG.WebKik.Web.Controllers.Project
{
    [RoutePrefix("api/projects")]
    public class ProjectController : EntityController<Models.Project, ProjectViewModel, int>
    {
        IFactShareCalculation factSharesService;
        IProjectCompanyShareService shareService;

        public ProjectController(IProjectService entityService, IFactShareCalculation factSharesService, IProjectCompanyShareService shareService) : base(entityService)
        {
            this.factSharesService = factSharesService;
            this.shareService = shareService;
        }

        [HttpGet, Route("{projectId}/ownership")] //todo: set filter.
        public async Task<ProjectOwnershipViewModel> GetProjectOwnership(int projectId, DateTime? date = null)
        {
            var project = await ((IProjectService)Service).GetProjectOwnership(projectId);
            var data = Mapper.Map<ProjectOwnershipViewModel>(project);

            var shares = await shareService.GetAllByProjectId(projectId, date ?? DateTime.Today);
            var factsShares = factSharesService.GetFactShares(shares);

            data.Links = factsShares
                .Where(x => x.ShareDirectPart > 0)
                .Select(x => new ProjectOwnershipLinkViewModel()
                {
                    SourceId = x.OwnerProjectCompanyId,
                    TargetId = x.DependentProjectCompanyId,
                    Share = x.ShareDirectPart

                }).ToArray();

            return data;
        }
    }
}
