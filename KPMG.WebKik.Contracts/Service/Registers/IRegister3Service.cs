using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Contracts.Service.Registers
{
    public interface IRegister3Service : IEntityService<Register3, int>, IRegisterBaseService<Register3>
    {
        Register3 GetRegister3ByCompanyId(int companyId, int year);
    }
}
