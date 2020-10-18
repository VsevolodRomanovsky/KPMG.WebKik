using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KPMG.WebKik.Models;
using KPMG.WebKik.Contracts.Service;
using System.Threading.Tasks;
using AutoMapper;
using System.IO;
using System.Net.Http.Headers;
using System.Web;

namespace KPMG.WebKik.Web.Controllers.TaxReturns
{
    [RoutePrefix("api/TaxReturn")]
    public class TaxReturnController : EntityController<TaxReturn, TaxReturnViewModel, int>
    {
        public TaxReturnController(ITaxReturnService entityService) : base(entityService)
        {
        }

        [HttpGet, Route("project/{projectId}")]
        public async Task<IEnumerable<TaxReturnViewModel>> GetByProjectId(int projectId)
        {
            var result = await ((ITaxReturnService)Service).GetByProjectId(projectId);
            return result.Select(Mapper.Map<TaxReturnViewModel>);
        }

        public override async Task<TaxReturnViewModel> Create([FromBody]TaxReturnViewModel model)
        {
            model.File = await GetDocument(model.ProjectCompanyId, model.Year);
            return await base.Create(model);
        }

        public override async Task Update([FromBody]TaxReturnViewModel model)
        {
            model.File = await GetDocument(model.ProjectCompanyId, model.Year);
            await base.Update(model);
        }

        [HttpGet, Route("{TaxReturnId}/file")]
        public async Task<HttpResponseMessage> GetFileById(int taxReturnId)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var entity = await ((ITaxReturnService)Service).GetById(taxReturnId);
            var stream = new MemoryStream(entity.File);
            result.Content = new StreamContent(stream);

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Document.xlsx"
            };

            return result;
        }

        private async Task<byte[]> GetDocument(int companyId, int year)
        {
            var path = HttpContext.Current.Server.MapPath("~/App_Data/TaxReturn.xlsx");

            return await ((ITaxReturnService)Service).GetDocument(companyId, path, year);
        }
    }
}
