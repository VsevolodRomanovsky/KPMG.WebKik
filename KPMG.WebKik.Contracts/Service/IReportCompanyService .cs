using KPMG.WebKik.Models;
using KPMG.WebKik.Models.ReportCompanies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KPMG.WebKik.Contracts.Service
{
    public interface IReportCompanyService
    {
        IEnumerable<CompanyChain> GetChains();
    }
}
