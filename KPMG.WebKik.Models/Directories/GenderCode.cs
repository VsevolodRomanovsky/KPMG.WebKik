using System.Collections.Generic;
using System.ComponentModel;
using KPMG.WebKik.Models.Companies;

namespace KPMG.WebKik.Models.Directories
{
    [DisplayName("Пол физических лиц")]
    public class GenderCode : IDirectoryEntry
    {
        public GenderCode()
        {
            IndividualCompanies = new HashSet<IndividualCompany>();
        }
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<IndividualCompany> IndividualCompanies { get; set; }
    }
}
