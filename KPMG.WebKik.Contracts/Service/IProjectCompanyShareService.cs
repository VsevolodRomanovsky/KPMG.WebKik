using System.Collections.Generic;
using System.Threading.Tasks;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Models;
using System;

namespace KPMG.WebKik.Contracts.Service
{
    public interface IProjectCompanyShareService : IEntityService<ProjectCompanyShare, int>
    {
        /// <summary>
        /// Получить список прямых владений на определенную дату
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<IList<ProjectCompanyShare>> GetAllByProjectCompanyId(int companyId, DateTime? date = null);

        Task<IList<ProjectCompanyShare>> GetAllByProjectId(int projectId, DateTime? date = null);

        Task<IList<ProjectCompany>> GetAllKIKsByProjectCompanyId(int companyId);

        /// <summary>
        /// Получить список фактических владений на определенную дату
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        Task<IList<ProjectCompanyFactShare>> GetFactByProjectCompanyId(int companyId, DateTime? date = null);
    }
}
