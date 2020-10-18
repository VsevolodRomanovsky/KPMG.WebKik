using System;
using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Web.Automapper;

namespace KPMG.WebKik.Web.Controllers.Company
{
    public class DocumentInformationViewModel: IEntity<int>
    {
        public int Id { get; set; }
        public int DocumentCodeId { get; set; }
        public string SeriesAndNumber { get; set; }
        public DateTimeOffset IssueDate { get; set; }
        public string IssuePlace { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<DocumentInformation, DocumentInformationViewModel>();
            cfg.CreateMap<DocumentInformationViewModel, DocumentInformation>()
                .ForMember(x => x.DocumentCode, y => y.Ignore())
                .ForMember(x => x.ConfirmedPersonalityDocInfo, y => y.Ignore())
                .ForMember(x => x.VerifedPersonalityDocInfo, y => y.Ignore());
        }
    }
}