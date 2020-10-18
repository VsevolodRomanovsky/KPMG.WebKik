using System.Collections.Generic;
using System.ComponentModel;
using KPMG.WebKik.Models.Companies;

namespace KPMG.WebKik.Models.Directories
{
    [DisplayName("Коды места жительства на территории Российской федерации")]
    public class RussianLocationCode : IDirectoryEntry
    {
        public RussianLocationCode()
        {
            IndividulaCompanies = new HashSet<IndividualCompany>();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<IndividualCompany> IndividulaCompanies { get; set; }
    }
}
