using KPMG.WebKik.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace KPMG.WebKik.Contracts.Service
{
    public interface INotificationOfParticipationService : IEntityService<NotificationOfParticipation, int>
    {
        Task<IEnumerable<NotificationOfParticipation>> GetByProjectId(int projectId);

        Task<byte[]> GetDocument(int companyId, int sigantoryId, string path);

        Task<MemoryStream> GetXMLDocument(int companyId, int sigantoryId, int correction);
    }
}
