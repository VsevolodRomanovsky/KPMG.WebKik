using System;
using AutoMapper.Configuration;
using KPMG.Webkik.Utils;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.ProjectCompanies;
using KPMG.WebKik.Web.Automapper;
using System.Collections.Generic;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;

namespace KPMG.WebKik.Web.Controllers.ProjectCompanyShare
{
    public class ProjectCompanyShareViewModel : IEntity<int>
    {
        public int Id { get; set; }

        public string CompanyStatus { get; set; }

        public ShareType ShareType { get; set; }

        public string ShareTypeName { get; set; }

        public double SharePart { get; set; }

        public DateTime ShareStartDate { get; set; }

        public DateTime? ShareFinishDate { get; set; }

        public double? ShareWithResidentsPart { get; set; }

        public double? ShareWithFamilyPart { get; set; }

        public int OwnerProjectCompanyId { get; set; }

        public bool? IsFounder { get; set; }

        public bool? IsControlledBy { get; set; }

        public bool? IsIndependentRecognition { get; set; }

        public DateTime? FoundDate { get; set; }

        public bool? IsOwnInterest { get; set; }
        public bool? IsPartnerInterest { get; set; }
        public bool? IsChildInterest { get; set; }
        public DateTime? ControlEmergenceDate { get; set; }
        public bool? IsNotificationOfParticipationRequired { get; set; }
        public GroundsType? ForeignLightCompanyGrounds { get; set; }
        public string ParticipantStatus { get; set; }

        public string ControlGrounds { get; set; }
        

        public ProjectCompanyViewModel OwnerProjectCompany { get; set; }
        public int DependentProjectCompanyId { get; set; }
        public ProjectCompanyViewModel DependentProjectCompany { get; set; }
        

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Models.ProjectCompanies.ProjectCompanyShare, ProjectCompanyShareViewModel>()
                .ForMember(x => x.ShareTypeName, o => o.MapFrom(s => s.ShareType.GetDescription()))
                .ForMember(x => x.ControlEmergenceDate, o => o.Ignore())
                .ForMember(x => x.ForeignLightCompanyGrounds, o => o.Ignore())
                .ForMember(x => x.ParticipantStatus, o => o.Ignore())
                ;

            cfg.CreateMap<ProjectCompanyShareViewModel, Models.ProjectCompanies.ProjectCompanyShare>()
                .ForMember(x => x.OwnerProjectCompany, y => y.Ignore())
                .ForMember(x => x.DependentProjectCompany, y => y.Ignore())               
                ;
        }
    }
}