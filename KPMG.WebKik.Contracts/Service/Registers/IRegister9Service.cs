using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Contracts.Service.Registers
{
    public interface IRegister9Service:  IRegisterBaseService<Register9>
    {
        Register9 GetRegister9ByCompanyId(int companyId, int year);
    }
}
