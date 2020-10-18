using KPMG.WebKik.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KPMG.WebKik.Contracts.Service
{
    public interface INotificationOfKIKService : IEntityService<NotificationOfKIK, int>
    {
        Task<IEnumerable<NotificationOfKIK>> GetByProjectId(int projectId);

        Task<byte[]> GetDocument(int companyId, int sigantoryId, string path, int year, int correction, int taxAuthorityCode);

        Task<MemoryStream> GetXMLDocument(int companyId, int sigantoryId, int year, int correction, int taxAuthorityCode);
    }
}
