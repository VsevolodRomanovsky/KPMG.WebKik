using System.ComponentModel;

namespace KPMG.WebKik.Models.Directories
{
    [DisplayName("Основания подачи уведомлений")]
    public class NotificationSubmissionGround : IDirectoryEntry
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
