using System;
using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;

namespace KPMG.WebKik.Web.Controllers.Company
{
    public class IndividualCompanyViewModel : IEntity<int>
    {
        public int Id { get; set; }

        public ProjectCompanyViewModel ProjectCompany { get; set; }

        public long INN { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public int? GenderCodeId { get; set; }
        public string BirthPlace { get; set; }
        public int? CitizenshipCodeId { get; set; }

        public int VerifedPersonalityDocInfoId { get; set; }
        public DocumentInformationViewModel VerifedPersonalityDocInfo { get; set; }
        public int ConfirmedPersonalityDocInfoId { get; set; }
        public DocumentInformationViewModel ConfirmedPersonalityDocInfo { get; set; }

        public int RussianLocationCodeId { get; set; }
        public int RegionCodeId { get; set; }
        public string PostIndex { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string CityType { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string BuildingNumber { get; set; }
        public string AppartamentNumber { get; set; }
        public int? ForeignCountryCodeId { get; set; }
        public string ForeignAddress { get; set; }


        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<IndividualCompany, IndividualCompanyViewModel>()
                .ForMember(x => x.ProjectCompany, y => y.Ignore());
            cfg.CreateMap<IndividualCompanyViewModel, IndividualCompany>()
                .ForMember(x => x.ProjectCompany, y => y.Ignore())
                .ForMember(x => x.GenderCode, y => y.Ignore())
                .ForMember(x => x.CitizenshipCode, y => y.Ignore())
                .ForMember(x => x.RussianLocationCode, y => y.Ignore())
                .ForMember(x => x.RegionCode, y => y.Ignore())
                .ForMember(x => x.ForeignCountryCode, y => y.Ignore());
        }
    }
}