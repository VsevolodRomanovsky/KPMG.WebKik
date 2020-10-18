using System.Web.Http;
using KPMG.WebKik.Contracts.Service.Registers;
using AutoMapper;
using KPMG.WebKik.Models.Registers;
using System.Threading.Tasks;
using KPMG.WebKik.Services.Registers;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Net.Http.Headers;
using System.Web;

namespace KPMG.WebKik.Web.Controllers.SupportingDocuments
{
    [RoutePrefix("api/supportingDocuments")]
    public class SupportingDocumentsController : ApiController
	{
		private ISupportingDocumentsService service;

		public SupportingDocumentsController(ISupportingDocumentsService service)
        {
			this.service = service;
		}

		[HttpGet, Route("{id:int}/type/{typeId:int}")]
		public ICollection<SupportingDocumentsViewModel> GetSupportingDocs(int id, int typeId)
		{
			var result = ((ISupportingDocumentsService)service).GetAllSupportingDocumentsByCompanyId(id, typeId);
			return Mapper.Map<ICollection<SupportingDocumentsViewModel>>(result);
		}
		//api/supportingDocuments/6/type/1

		[HttpGet, Route("{id:int}/file")]
		public async Task<HttpResponseMessage> GetSupportingDocById(int id)
		{
			var result = new HttpResponseMessage(HttpStatusCode.OK);
			var entity = service.GetById(id);
			var extension = entity.FileName.Split('.');
			string ext = string.Empty;
			if (extension.Count() > 0)
			{
				ext = extension[extension.Count() - 1];
			}
			result.Content = new ByteArrayContent(entity.Data);
			if (ext.Equals("xlsx") || ext.Equals("xls"))
			{
				result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
			}
			else if (ext.Equals("doc") || ext.Equals("docx"))
			{
				result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/msword");
			}
			else if (ext.Equals("pdf"))
			{
				result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

			}
			else
				result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

			result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
			{
				FileName = entity.FileName
			};

			return result;
		}
	}
}
