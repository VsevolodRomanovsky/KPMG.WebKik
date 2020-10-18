using KPMG.WebKik.Models.Directories;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models
{
    public class Signatory : IEntity<int>
    {
        public int Id { get; set; }
        public int ProjectCompanyId { get; set; }
        public ProjectCompany ProjectCompany { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int SignatoryCodeId { get; set; }
        public SignatoryCode SignatoryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ConfirmationDocumentId { get; set; }
        public DocumentCode ConfirmationDocument { get; set; }
        public string Inn { get; set; }

        public string GetFio()
        {
            return string.Join(" ", this.LastName, this.FirstName, this.MiddleName);
        }
    }
}
