using System.Collections.Generic;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Registers;
using System.Threading.Tasks;

namespace KPMG.WebKik.Contracts.Service
{
	public interface ISupportingDocumentsService 
	{
		// SupportingDocument CreateDocument();

		SupportingDocument DownloadDocument();
		SupportingDocument CreateDocument(int year, int companyType, int companyId, bool isUU, bool isUKIK, bool isND, string uKIKDocType, byte[] fileData, string fileName);
		ICollection<SupportingDocument> GetSupportingDocumentsByCompanyId(int id);

		ICollection<SupportingDocument> GetAllSupportingDocumentsByCompanyId(int id, int type);

		SupportingDocument GetById(int id);
	}
}
