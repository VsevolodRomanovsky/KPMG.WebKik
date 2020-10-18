using System;
using System.ComponentModel.DataAnnotations;
using KPMG.WebKik.Models.Directories;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models.Companies
{
    public class IndividualCompany : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public ProjectCompany ProjectCompany { get; set; }

        public long INN { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public int? GenderCodeId { get; set; }
        public GenderCode GenderCode { get; set; }
        public string BirthPlace { get; set; }
        public int? CitizenshipCodeId { get; set; }
        public CitizenshipCode CitizenshipCode { get; set; }

        public int VerifedPersonalityDocInfoId { get; set; }
        public DocumentInformation VerifedPersonalityDocInfo { get; set; }
        public int ConfirmedPersonalityDocInfoId { get; set; }
        public DocumentInformation ConfirmedPersonalityDocInfo { get; set; }

        public int RussianLocationCodeId { get; set; }
        public RussianLocationCode RussianLocationCode { get; set; }
        public int RegionCodeId { get; set; } 
        public RegionCode RegionCode { get; set; }
        public string PostIndex { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string CityType { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string BuildingNumber { get; set; }
        public string AppartamentNumber { get; set; }
        public int? ForeignCountryCodeId { get; set; }
        public CountryCode ForeignCountryCode { get; set; }
        public string ForeignAddress { get; set; }
    }
}
