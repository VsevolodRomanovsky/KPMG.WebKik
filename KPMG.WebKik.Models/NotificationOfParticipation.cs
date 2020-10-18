using System;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Models.Directories;

namespace KPMG.WebKik.Models
{
    public class NotificationOfParticipation : IEntity<int>
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int ProjectCompanyId { get; set; }
        public ProjectCompany ProjectCompany { get; set; }
        public int Correction { get; set; }
        public int SubmissionGroundId { get; set; }
        public NotificationSubmissionGround SubmissionGround { get; set; }
        public int SignatoryId { get; set; }
        public Signatory Signatory { get; set; }
        public DateTimeOffset? SubmissionDate { get; set; }
        public byte[] File { get; set; }
    }
}
