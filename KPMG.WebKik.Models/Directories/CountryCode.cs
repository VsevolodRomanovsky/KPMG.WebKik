using System.Collections.Generic;
using System.ComponentModel;
using KPMG.WebKik.Models.Companies;

namespace KPMG.WebKik.Models.Directories
{
    [DisplayName("Общероссийский классификатор стран мира")]
    public class CountryCode : IDirectoryEntry
    {
        public CountryCode()
        {
            ForeignCompanies = new HashSet<ForeignCompany>();
            ForeignLightCompaies = new HashSet<ForeignLightCompany>();
            IndividualCompanies = new HashSet<IndividualCompany>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string FullName { get; set; }
        public ICollection<ForeignCompany> ForeignCompanies { get; set; }
        public ICollection<ForeignLightCompany> ForeignLightCompaies { get; set; }
        public ICollection<IndividualCompany> IndividualCompanies { get; set; }
    }
}
