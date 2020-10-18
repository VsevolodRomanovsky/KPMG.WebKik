using System.Collections.Generic;
using System.ComponentModel;
using KPMG.WebKik.Models.Companies;

namespace KPMG.WebKik.Models.Directories
{
    [DisplayName("Коды организационных форм иностранных структур")]
    public class ForeignOrganizationalFormCode : IDirectoryEntry
    {
        public ForeignOrganizationalFormCode()
        {
            ForeignLightCompanies = new HashSet<ForeignLightCompany>();
        }
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<ForeignLightCompany> ForeignLightCompanies { get; set; }
    }
}
