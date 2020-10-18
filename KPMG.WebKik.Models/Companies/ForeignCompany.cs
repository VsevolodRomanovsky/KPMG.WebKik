using System;
using System.ComponentModel.DataAnnotations;
using KPMG.WebKik.Models.Directories;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models.Companies
{
    public class ForeignCompany : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public ProjectCompany ProjectCompany { get; set; }
        public int CountryCodeId { get; set; }
        public CountryCode CountryCode { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string RegistrationNumber { get; set; }
        public int? TaxPayerCodeId { get; set; }
        public TaxPayerCode TaxPayerCode { get; set; }
        public string Address { get; set; }
        public DateTimeOffset FoundDate { get; set; }
    }
}
