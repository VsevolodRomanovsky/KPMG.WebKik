using System;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models
{
    public class NotificationOfKIK: IEntity<int>
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int ProjectCompanyId { get; set; }
        public ProjectCompany ProjectCompany { get; set; }
        public int SignatoryId { get; set; }
        public Signatory Signatory { get; set; }
        public int Correction { get; set; }
        public int TaxAuthorityCode { get; set; }
        public byte[] File { get; set; }
    }
}
