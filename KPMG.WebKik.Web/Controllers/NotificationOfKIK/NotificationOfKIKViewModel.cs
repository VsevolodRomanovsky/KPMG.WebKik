using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;
using System;

namespace KPMG.WebKik.Web.Controllers.NotificationOfKIKs
{
    public class NotificationOfKIKViewModel : IEntity<int>
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int ProjectCompanyId { get; set; }
        public int Year { get; set; }
        public int SignatoryId { get; set; }
        public Signatory Signatory { get; set; }
        public int Correction { get; set; }
        public int TaxAuthorityCode { get; set; }

        public ProjectCompanyViewModel ProjectCompany { get; set; }
        public byte[] File { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<NotificationOfKIK, NotificationOfKIKViewModel>()
                .ForMember(x => x.File, o => o.Ignore())
                ;

            cfg.CreateMap<NotificationOfKIKViewModel, NotificationOfKIK>()
                .ForMember(x => x.ProjectCompany, o => o.Ignore())
                .ForMember(x => x.Signatory, o => o.Ignore())
                .ForMember(x => x.Correction, o => o.Ignore())
                ;
        }
    }
}