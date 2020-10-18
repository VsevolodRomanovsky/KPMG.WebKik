using KPMG.WebKik.Models.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPMG.WebKik.Contracts.Service.Registers
{
    public interface IRegisterService
    {
        Task<IEnumerable<RegisterListItem>> GetRegistersByShareIdAndYear(int projectCompanyShareId, int year);
    }
}
