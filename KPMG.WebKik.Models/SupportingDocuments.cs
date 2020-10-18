using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Models.ProjectCompanies;
using System;

namespace KPMG.WebKik.Models
{
    public class SupportingDocument : IEntity<int>
    {
		public int Id { get; set; }

		public int Year { get; set; }

		public int CompanyType { get; set; }

		public int ProjectCompanyId { get; set; }

		public ProjectCompany ProjectCompany { get; set; }

		public bool IsUU { get; set; }


		public bool IsUKIK { get; set; }

		public bool IsND { get; set; }

		public int? UKIKDocType { get; set; }

		public string FileName { get; set; }

		public byte[] Data { get; set; }
    }
}
