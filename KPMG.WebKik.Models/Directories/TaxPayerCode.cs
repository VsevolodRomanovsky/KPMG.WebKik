using System.Collections.Generic;
using System.ComponentModel;
using KPMG.WebKik.Models.Companies;

namespace KPMG.WebKik.Models.Directories
{
    [DisplayName("Коды налогоплательщика")]
    public class TaxPayerCode : IDirectoryEntry
    {
        public TaxPayerCode()
        {
            ForeignCompanies = new HashSet<ForeignCompany>();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<ForeignCompany> ForeignCompanies { get; set; }
    }
}
