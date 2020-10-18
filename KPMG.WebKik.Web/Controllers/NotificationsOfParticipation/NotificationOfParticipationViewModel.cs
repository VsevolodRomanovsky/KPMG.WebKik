using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Directories;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;
using System;

namespace KPMG.WebKik.Web.Controllers.NotificationsOfParticipation
{
    public class NotificationOfParticipationViewModel : IEntity<int>
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int ProjectCompanyId { get; set; }
        public ProjectCompanyViewModel ProjectCompany { get; set; }
        public int Correction { get; set; }
        public int SubmissionGroundId { get; set; }
        public NotificationSubmissionGround SubmissionGround { get; set; }
        public int SignatoryId { get; set; }
        public Signatory Signatory { get; set; }
        public DateTimeOffset? SubmissionDate { get; set; }
        public byte[] File { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<NotificationOfParticipation, NotificationOfParticipationViewModel>()
                .ForMember(x => x.File, o => o.Ignore())
                ;

            cfg.CreateMap<NotificationOfParticipationViewModel, NotificationOfParticipation>()
                .ForMember(x => x.ProjectCompany, o => o.Ignore())
                .ForMember(x => x.SubmissionGround, o => o.Ignore())
                .ForMember(x => x.Signatory, o => o.Ignore())
                ;
        }
    }
}
