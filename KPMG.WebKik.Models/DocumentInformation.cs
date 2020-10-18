using System;
using System.Collections.Generic;
using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Models.Directories;

namespace KPMG.WebKik.Models
{
    public class DocumentInformation : IEntity<int>
    {
        public DocumentInformation()
        {
            VerifedPersonalityDocInfo = new HashSet<IndividualCompany>();
            ConfirmedPersonalityDocInfo = new HashSet<IndividualCompany>();
        }
        public int Id { get; set; }
        public int DocumentCodeId { get; set; }
        public DocumentCode DocumentCode { get; set; }
        public string SeriesAndNumber { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public string IssuePlace { get; set; }

        public ICollection<IndividualCompany> VerifedPersonalityDocInfo { get; set; }
        public ICollection<IndividualCompany> ConfirmedPersonalityDocInfo { get; set; }
    }
}
