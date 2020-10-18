using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Contracts.Service.Registers
{
    public interface IRegister11Service:  IRegisterBaseService<Register11>
    {

		Register11 GetRegister11(int id);

		Register11Data CreateRegisterData(Register11Data data);

		Register11Data EditRegisterData(Register11Data data);

		Register11Data DeleteRegisterData(int id);

		Register11 Create(Register11 model);
	}
}
