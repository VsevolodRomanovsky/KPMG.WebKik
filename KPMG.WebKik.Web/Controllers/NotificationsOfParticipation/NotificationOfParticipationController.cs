using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using KPMG.WebKik.Contracts.Service;
using System.Collections.Generic;
using System.Web;
using KPMG.WebKik.Models;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace KPMG.WebKik.Web.Controllers.NotificationsOfParticipation
{
    [RoutePrefix("api/notificationsofparticipation")]
    public class NotificationOfParticipationController : EntityController<NotificationOfParticipation, NotificationOfParticipationViewModel, int>
    {
        public NotificationOfParticipationController(INotificationOfParticipationService entityService) : base(entityService)
        {
        }

        [HttpGet, Route("project/{projectId}")]
        public async Task<IEnumerable<NotificationOfParticipationViewModel>> GetByProjectId(int projectId)
        {
            var result = await ((INotificationOfParticipationService)Service).GetByProjectId(projectId);
            return result.Select(Mapper.Map<NotificationOfParticipationViewModel>);
        }

        public override async Task<NotificationOfParticipationViewModel> Create([FromBody]NotificationOfParticipationViewModel model)
        {
            model.File = await GetDocument(model.ProjectCompanyId, model.SignatoryId);
            return await base.Create(model);
        }


        public override async Task Update([FromBody]NotificationOfParticipationViewModel model)
        {
            model.File = await GetDocument(model.ProjectCompanyId, model.SignatoryId);

            await base.Update(model);
        }

        [HttpGet, Route("{notificationId}/file")]
        public async Task<HttpResponseMessage> GetFileById(int notificationId)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var entity = await ((INotificationOfParticipationService)Service).GetById(notificationId);
            var stream = new MemoryStream(entity.File);
            result.Content = new StreamContent(stream);

            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Document.xlsx"
            };

            return result;
        }

        [HttpGet, Route("{notificationId}/xmlfile")]
        public async Task<HttpResponseMessage> GetXMLFileById(int notificationId)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var entity = await ((INotificationOfParticipationService)Service).GetById(notificationId);
            var stream = await ((INotificationOfParticipationService)Service).GetXMLDocument(entity.ProjectCompanyId, entity.SignatoryId, entity.Correction);
            result.Content = new StreamContent(stream);
            
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "Document.xml"
            };

            return result;
        }

        private async Task<byte[]> GetDocument(int companyId, int sigantoryId)
        {
            var path = HttpContext.Current.Server.MapPath("~/App_Data/UU.xlsx");
            return await ((INotificationOfParticipationService)Service).GetDocument(companyId, sigantoryId, path);
        }
    }
}
