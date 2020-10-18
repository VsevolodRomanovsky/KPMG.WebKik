using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Models;
using System.Web.Http;
using System.Threading.Tasks;
using AutoMapper;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Net.Http.Headers;

namespace KPMG.WebKik.Web.Controllers.NotificationOfKIKs
{
    [RoutePrefix("api/NotificationOfKIK")]
    public class NotificationOfKIKController : EntityController<NotificationOfKIK, NotificationOfKIKViewModel, int>
    {
        public NotificationOfKIKController(INotificationOfKIKService entityService) : base(entityService)
        {
        }

        [HttpGet, Route("project/{projectId}")]
        public async Task<IEnumerable<NotificationOfKIKViewModel>> GetByProjectId(int projectId)
        {
            var result = await ((INotificationOfKIKService)Service).GetByProjectId(projectId);
            return result.Select(Mapper.Map<NotificationOfKIKViewModel>);
        }

        public override async Task<NotificationOfKIKViewModel> Create([FromBody]NotificationOfKIKViewModel model)
        {
            model.File = await GetDocument(model.ProjectCompanyId, model.SignatoryId, model.Year, model.Correction, model.TaxAuthorityCode);
            return await base.Create(model);
        }

        public override async Task Update([FromBody]NotificationOfKIKViewModel model)
        {
            model.File = await GetDocument(model.ProjectCompanyId, model.SignatoryId, model.Year, model.Correction, model.TaxAuthorityCode);
            await base.Update(model);
        }

        [HttpGet, Route("{NotificationOfKIKId}/file")]
        public async Task<HttpResponseMessage> GetFileById(int notificationOfKIKId)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var entity = await ((INotificationOfKIKService)Service).GetById(notificationOfKIKId);
            var stream = new MemoryStream(entity.File);
            result.Content = new StreamContent(stream);

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Document.xlsx"
            };

            return result;
        }

        private async Task<byte[]> GetDocument(int companyId, int sigantoryId, int year, int correction, int taxAuthorityCode)
        {
            
            var path = HttpContext.Current.Server.MapPath("~/App_Data/CFC_Notification_template_OK.xlsx");

            return await ((INotificationOfKIKService)Service).GetDocument(companyId, sigantoryId, path, year, correction, taxAuthorityCode);
        }

        [HttpGet, Route("{NotificationOfKIKId}/xmlfile")]
        public async Task<HttpResponseMessage> GetXMLFileById(int notificationOfKIKId)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var entity = await ((INotificationOfKIKService)Service).GetById(notificationOfKIKId);
            var stream = await ((INotificationOfKIKService)Service).GetXMLDocument(entity.ProjectCompanyId, entity.SignatoryId, entity.Year, entity.Correction, entity.TaxAuthorityCode);
            result.Content = new StreamContent(stream);

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Document.xml"
            };

            return result;
        }
    }
}