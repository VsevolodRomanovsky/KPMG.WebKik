using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models.Registers
{
    public interface IRegister : IEntity<int>
    {
        Year Year { get; set; }
        RegisterType Type { get; set; }
        int OwnerProjectCompanyId { get; set; }
        ProjectCompany OwnerProjectCompany { get; set; }
    }
}
