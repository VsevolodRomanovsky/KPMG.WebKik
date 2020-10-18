using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models.Companies
{
    public class DomesticCompany : IEntity<int>
    {
        [Key]
        public int Id { get; set; }
        public ProjectCompany ProjectCompany { get; set; }
        public string Number { get; set; }
        public string FullName { get; set; }
        public long OGRN { get; set; }
        public long INN { get; set; }
        public string KPP { get; set; }
        public bool IsPublic { get; set; }
    }
}
