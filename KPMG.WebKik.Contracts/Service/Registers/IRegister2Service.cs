using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Contracts.Service.Registers
{
    public interface IRegister2Service : IEntityService<Register2, int>, IRegisterBaseService<Register2>
    {
        Register2 GetRegister2ByCompanyId(int companyId, int year);
    }
}
