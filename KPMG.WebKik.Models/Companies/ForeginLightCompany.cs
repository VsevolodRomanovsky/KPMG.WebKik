using System;
using System.ComponentModel.DataAnnotations;
using KPMG.WebKik.Models.Directories;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models.Companies
{
    public class ForeignLightCompany : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public ProjectCompany ProjectCompany { get; set; }
        public string Number { get; set; }
        public int ForeignOrganizationalFormCodeId { get; set; }
        public ForeignOrganizationalFormCode ForeignOrganizationalFormCode { get; set; }
        public string EnglishName { get; set; }
        public string RussianName { get; set; }
        public DateTimeOffset FoundDate { get; set; }
        public string RequisitesEng { get; set; }
        public string RequisitesRus { get; set; }
        public int CountryCodeId { get; set; }
        public CountryCode CountryCode { get; set; }
        public string RegNumber { get; set; }
        public string OtherInfo { get; set; }
    }
}
