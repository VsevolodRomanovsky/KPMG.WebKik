using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Contracts.Service.Registers
{
    public interface IRegister1Service : IEntityService<Register1, int>, IRegisterBaseService<Register1>
    {
        Register1 GetRegister1ByCompanyId(int companyId, int year);
    }
}
