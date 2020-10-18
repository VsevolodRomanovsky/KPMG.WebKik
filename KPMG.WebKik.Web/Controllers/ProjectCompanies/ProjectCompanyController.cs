using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using KPMG.Webkik.Utils;
using KPMG.WebKik.Contracts.Service;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Models;
using KPMG.WebKik.Web.Controllers.ProjectCompanyShare;
using System;

namespace KPMG.WebKik.Web.Controllers.ProjectCompanies
{

    [RoutePrefix("api/companies")]
	public class ProjectCompanyController : EntityController<ProjectCompany, ProjectCompanyViewModel, int>
	{

		public ProjectCompanyController(IProjectCompanyService entityService) : base(entityService)
		{
		}

	    [HttpGet, Route("project/{projectId}")]
		public async Task<IQueryable<ProjectCompanyViewModel>> GetCompaniesByProjectId(int projectId)
		{
			var companies = await ((IProjectCompanyService)Service).GetAllByProjectId(projectId);
			return companies.AsQueryable().ProjectTo<ProjectCompanyViewModel>();
		}

		[HttpGet, Route("project/{projectId}/calculate")]
		public async Task<int> CalculateProjectCompanyInfo(int projectId)
		{
			await ((IProjectCompanyService)Service).CalculateProjectInfo(projectId);

			return 1;
		}

		[HttpGet, Route("states")]
		public IEnumerable<KeyValuePair<State, string>> GetCompanyStates()
		{
			return EnumHelper.ToKeyValuePairs<State>();
		}

		[HttpGet, Route("groundstypes")]
		public IEnumerable<KeyValuePair<GroundsType, string>> GetGroundTypes()
		{
			return EnumHelper.ToKeyValuePairs<GroundsType>();
		}

		[HttpGet, Route("project/{projectId}/companies-for-notification")]
		public async Task<IEnumerable<ProjectCompanyViewModel>> GetCompaniesForNotification(int projectId)
		{
			var result = await ((IProjectCompanyService)Service).GetCompaniesForNotification(projectId);
			return result.Select(Mapper.Map<ProjectCompanyViewModel>);
		}

        [HttpPost, Route("uploadKikRegisters")]
	    public async Task<HttpResponseMessage> UploadKikRegisters()
	    {
	        var provider = new FormDataStreamProvider();
	        await Request.Content.ReadAsMultipartAsync(provider);

	        var sharedId = provider.FormData["sharedId"];
	        var year = provider.FormData["year"];

	        if (!provider.Files.Any())
	        {
	            return Request.CreateResponse(HttpStatusCode.BadRequest, "Файлы отсутствуют.");
	        }

	        IList<string> uploadedFiles = new List<string>();

	        foreach (var file in provider.Files)
	        {
	            var fileName = file.Key;
	            var fileBody = file.Value;
	            ((IProjectCompanyService)Service).UploadDataFromExcel(fileBody, Convert.ToInt32(sharedId), Convert.ToInt32(year));
	            uploadedFiles.Add(fileName);
	        }

	        return Request.CreateResponse(HttpStatusCode.OK, "Данные импортированы: " + string.Join(", ", uploadedFiles));
	    }


        [HttpPost, Route("uploadKlRegisters")]
	    public async Task<HttpResponseMessage> UploadKlRegisters()
	    {
	        var provider = new FormDataStreamProvider();
	        await Request.Content.ReadAsMultipartAsync(provider);

	        var sharedId = provider.FormData["sharedId"];
	        var year = provider.FormData["year"];

	        if (!provider.Files.Any())
	        {
	            return Request.CreateResponse(HttpStatusCode.BadRequest, "Файлы отсутствуют.");
	        }

	        IList<string> uploadedFiles = new List<string>();

	        foreach (var file in provider.Files)
	        {
	            var fileName = file.Key;
	            var fileBody = file.Value;
	            ((IProjectCompanyService)Service).UploadKlDataFromExcel(fileBody, Convert.ToInt32(sharedId), Convert.ToInt32(year));
	            uploadedFiles.Add(fileName);
	        }

	        return Request.CreateResponse(HttpStatusCode.OK, "Данные импортированы: " + string.Join(", ", uploadedFiles));
	    }


	    [HttpPost, Route("definestatus")]
		public async Task<TaxExemptionResult> DefineTaxStatus([FromBody]TaxExemptionViewModel model)
		{
			var entity = Mapper.Map<TaxExemption>(model);
			var result = await (Service as IProjectCompanyService).DefineTaxStatus(entity);
			return result;
		}

		[HttpGet, Route("{companyId}/tax-exemptions/{toCompanyId}/{year}")]
		public TaxExemptionViewModel GetTaxExeption(int companyId, int toCompanyId, int year)
		{
			var result = ((IProjectCompanyService)Service).TaxExemptionFor(companyId, toCompanyId, year);
			return Mapper.Map<TaxExemptionViewModel>(result);
		}

		[HttpPost, Route("upload")]
		public async Task<HttpResponseMessage> UploadFile()
		{
		    var provider = new FormDataStreamProvider();
		    await Request.Content.ReadAsMultipartAsync(provider);

		    int year;
		    int.TryParse(provider.FormData["year"], out year);
            var companyType = int.Parse(provider.FormData["companyType"]);
            var isUU = !string.IsNullOrEmpty(provider.FormData["isUU"]) && bool.Parse(provider.FormData["isUU"]);
            var isUKIK = !string.IsNullOrEmpty(provider.FormData["isUKIK"]) && bool.Parse(provider.FormData["isUKIK"]);
            var isND = !string.IsNullOrEmpty(provider.FormData["isND"]) && bool.Parse(provider.FormData["isND"]);
            var UKIKDocType = provider.FormData["UKIKDocType"];
            var companyId = int.Parse(provider.FormData["companyId"]);

		    if (!provider.Files.Any())
		    {
		        return Request.CreateResponse(HttpStatusCode.BadRequest, "Файлы отсутствуют.");
		    }

		    IList<string> uploadedFiles = new List<string>();

		    foreach (var file in provider.Files)
		    {
		        var fileName = file.Key;
		        var fileBody = file.Value;
		        ((IProjectCompanyService)Service).UploadFile(year, companyType, companyId, isUU, isUKIK, isND, UKIKDocType, fileBody, fileName);
		        uploadedFiles.Add(fileName);
		    }
		    return Request.CreateResponse(HttpStatusCode.OK, "Данные импортированы: " + string.Join(", ", uploadedFiles));
		}

	    [HttpPost, Route("individualCompanyImport")]
	    public async Task<HttpResponseMessage> UploadIndividualCompanyFile()
	    {
            // если послали что-то плохое и сервер этого не понимает
	        //if (!Request.Content.IsMimeMultipartContent())
	        //{
	        //    return Request.CreateResponse(HttpStatusCode.UnsupportedMediaType, "Сервер не поддерживает данный тип данных.");
	        //}

	        var provider = new FormDataStreamProvider();
	        await Request.Content.ReadAsMultipartAsync(provider);
	        var projectId = provider.FormData["projectId"];

	        //тak проверить тип файла если будет отправляться с клиента
	        //int uploadType;
            //if (!int.TryParse(provider.FormData["uploadType"], out uploadType))
            //{
            //   return Request.CreateResponse(HttpStatusCode.BadRequest, "Upload Type is invalid.");
            //}

            if (!provider.Files.Any())
	        {
	            return Request.CreateResponse(HttpStatusCode.BadRequest, "Файлы отсутствуют.");
	        }

	        IList<string> uploadedFiles = new List<string>();

            foreach (var file in provider.Files)
	        {
	            var fileName = file.Key;
	            var fileBody = file.Value;

                // импорт данных
	            ((IProjectCompanyService)Service).IndividualUploadDataFromExcel(fileBody, Convert.ToInt32(projectId));
	           
                // сохранить для ответа
                uploadedFiles.Add(fileName);
	        }

	        return Request.CreateResponse(HttpStatusCode.OK, "Данные импортированы: " + string.Join(", ", uploadedFiles));
	    }


	    [HttpPost, Route("domesticCompanyImport")]
	    public async Task<HttpResponseMessage> UploadDomesticCompanyFile()
	    {
	        var provider = new FormDataStreamProvider();
	        await Request.Content.ReadAsMultipartAsync(provider);


	        var projectId = provider.FormData["projectId"];

	        if (!provider.Files.Any())
	        {
	            return Request.CreateResponse(HttpStatusCode.BadRequest, "Файлы отсутствуют.");
	        }

	        IList<string> uploadedFiles = new List<string>();

	        foreach (var file in provider.Files)
	        {
	            var fileName = file.Key;
	            var fileBody = file.Value;
	            ((IProjectCompanyService)Service).DomesticUploadDataFromExcel(fileBody, Convert.ToInt32(projectId));
	            uploadedFiles.Add(fileName);
	        }

	        return Request.CreateResponse(HttpStatusCode.OK, "Данные импортированы: " + string.Join(", ", uploadedFiles));
	        
	    }

	    [HttpPost, Route("foreignLightCompanyImport")]
	    public async Task<HttpResponseMessage> UploadForeignLigtCompanyCompanyFile()
	    {
	        var provider = new FormDataStreamProvider();
	        await Request.Content.ReadAsMultipartAsync(provider);

	        var projectId = provider.FormData["projectId"];

	        if (!provider.Files.Any())
	        {
	            return Request.CreateResponse(HttpStatusCode.BadRequest, "Файлы отсутствуют.");
	        }

	        IList<string> uploadedFiles = new List<string>();

	        foreach (var file in provider.Files)
	        {
	            var fileName = file.Key;
	            var fileBody = file.Value;
	            ((IProjectCompanyService)Service).ForeignLightUploadDataFromExcel(fileBody, Convert.ToInt32(projectId));
                uploadedFiles.Add(fileName);
	        }

	        return Request.CreateResponse(HttpStatusCode.OK, "Данные импортированы: " + string.Join(", ", uploadedFiles));
	    }

	    [HttpPost, Route("foreignCompanyImport")]
	    public async Task<HttpResponseMessage> UploadForeignCompanyCompanyFile()
	    {

	        var provider = new FormDataStreamProvider();
	        await Request.Content.ReadAsMultipartAsync(provider);

	        var projectId = provider.FormData["projectId"];

	        if (!provider.Files.Any())
	        {
	            return Request.CreateResponse(HttpStatusCode.BadRequest, "Файлы отсутствуют.");
	        }

	        IList<string> uploadedFiles = new List<string>();

	        foreach (var file in provider.Files)
	        {
	            var fileName = file.Key;
	            var fileBody = file.Value;
	            ((IProjectCompanyService)Service).ForeignUploadDataFromExcel(fileBody, Convert.ToInt32(projectId));
	            uploadedFiles.Add(fileName);
	        }

	        return Request.CreateResponse(HttpStatusCode.OK, "Данные импортированы: " + string.Join(", ", uploadedFiles));
	        

	    }
	}
}