using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using KPMG.WebKik.Contracts.Service;
using AutoMapper;
using KPMG.WebKik.Models;
using System;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;

namespace KPMG.WebKik.Web.Controllers.ProjectCompanyShare
{
    [RoutePrefix("api/shares")]
    public class ProjectCompanyShareController : EntityController<Models.ProjectCompanies.ProjectCompanyShare, ProjectCompanyShareViewModel, int>
    {
        public ProjectCompanyShareController(IProjectCompanyShareService entityService) : base(entityService)
        {

        }

        [HttpGet, Route("company/{companyId}")]
        public async Task<IQueryable<ProjectCompanyShareViewModel>> GetSharesByCompanyId(int companyId)
        {
            var companies = await (Service as IProjectCompanyShareService).GetAllByProjectCompanyId(companyId);
            return companies.AsQueryable().ProjectTo<ProjectCompanyShareViewModel>();
        }

        [HttpGet, Route("company/{companyId}/fact")]
        public async Task<IQueryable<ProjectCompanyFactShareViewModel>> GetFactSharesByCompanyId(int companyId, DateTime? date = null)
        {
            var companies = await (Service as IProjectCompanyShareService).GetFactByProjectCompanyId(companyId, date ?? DateTime.Now);
            return companies.AsQueryable().ProjectTo<ProjectCompanyFactShareViewModel>();
        }

        [HttpGet, Route("company/{companyId}/kik")]
        public async Task<IQueryable<ProjectCompanyViewModel>> GetKIKsByCompanyId(int companyId)
        {
            var companies = await (Service as IProjectCompanyShareService).GetAllKIKsByProjectCompanyId(companyId);
            return companies.AsQueryable().ProjectTo<ProjectCompanyViewModel>();
        }

        
    }
}
