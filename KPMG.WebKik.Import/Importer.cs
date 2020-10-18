using KPMG.WebKik.Models;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using KPMG.Webkik.Utils;
using KPMG.WebKik.Models.Companies;
using KPMG.WebKik.Models.Directories;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Import
{
    public class Importer
    {
        private readonly DbContext context;

        public Importer(DbContext context)
        {
            this.context = context;
        }

        public DirectoryList GetDirectoriesData(string filePath)
        {
            var fileText = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath));
            return JsonConvert.DeserializeObject<DirectoryList>(fileText);
        }

        public void ImportDirectories(DirectoryList data)
        {
            var assemblies = new[] { typeof(IDirectoryEntry).Assembly };
            var directoryEntryTypes = ReflectionHelper.GetTypeByInterface<IDirectoryEntry>(assemblies).ToList();

            foreach (var directory in data.Directories)
            {
                var directoryEntryType = directoryEntryTypes.Single(x => x.Name == directory.Name);
                foreach (var entry in directory.Entries)
                {
                    var directoryEntry = Activator.CreateInstance(directoryEntryType) as dynamic;
                    directoryEntry.Code = entry.Code;
                    directoryEntry.Name = entry.Name;
                    if (directoryEntryType == typeof(CountryCode))
                    {
                        directoryEntry.Code1 = entry.Code1;
                        directoryEntry.Code2 = entry.Code2;
                        directoryEntry.FullName = entry.FullName;
                    }
                    context.Set(directoryEntryType).Add(directoryEntry);
                }
            }
        }

        public IList<Role> GetRoles()
        {
            return new List<Role>
            {
                new Role { Name = Role.Employee },
                new Role { Name = Role.Administrator }
            };
        }

        public IList<User> GetUsers(IList<Role> roles)
        {
            return new List<User>
            {
                new User { UserLogin = "login1", DisplayName = "User 1", Role = roles[1] },
                new User { UserLogin = "login2", DisplayName = "User 2", Role = roles[1] },
                new User { UserLogin = "login3", DisplayName = "User 3", Role = roles[1] },
                new User { UserLogin = "login4", DisplayName = "User 4", Role = roles[1] },
                new User { UserLogin = "login5", DisplayName = "User 5", Role = roles[1] },
                new User { UserLogin = "login6", DisplayName = "User 6", Role = roles[1] },
                new User { UserLogin = "login7", DisplayName = "User 7", Role = roles[1] },
            };
        }

        public IList<Project> GetProjects(IList<User> users)
        {
            return new List<Project>
            {
                new Project { Name = "Группа компаний \"Группа\"", Description = "Группа для проведения демонстрации", CreationDate = DateTime.UtcNow, Users = users },
                new Project { Name = "Группа 2", Description = "", CreationDate = DateTime.UtcNow, Users = new List<User> { users[0] }},
                new Project { Name = "Группа 3", Description = "", CreationDate = DateTime.UtcNow, Users = new List<User> { users[1] }},
                new Project { Name = "Группа 4", Description = "", CreationDate = DateTime.UtcNow, Users = new List<User> {  }},
            };
        }

        public IList<ProjectCompany> GetCompanies(IList<Project> projects)
        {
            var p = projects[0];
            var modifiedDate = new DateTime(2016, 12, 06);

            var docInfo = new DocumentInformation { DocumentCodeId = 11, SeriesAndNumber = "4513105699", IssueDate = DateTime.UtcNow, IssuePlace = "УФМС России по гор.Москве по району Басманный" };
            var ic = new IndividualCompany { INN = 1212121333, Surname = "Иванов", Name = "Иван", MiddleName = "Иванович", BirthDate = new DateTime(1970, 1, 1), GenderCodeId = 1, BirthPlace = "Москва", VerifedPersonalityDocInfo = docInfo, ConfirmedPersonalityDocInfo = docInfo, RussianLocationCodeId = 1, RegionCodeId = 77, PostIndex = "105094", District = "Басманный", City = "Москва", CityType = "", Street = "Госпитальная набережная", HouseNumber = "4", BuildingNumber = "1а", AppartamentNumber = "125", ForeignCountryCodeId = 185, ForeignAddress = "" };

            var flc = new ForeignLightCompany { Number = "ИО4", ForeignOrganizationalFormCodeId = 5, EnglishName = "International company", RussianName = "Интернейшнл кампани", FoundDate = new DateTime(2010, 12, 12), RequisitesEng = "Regulation 45", RequisitesRus = "Устав 45", CountryCodeId = 39, RegNumber = "1212121", OtherInfo = "-" };

            var dc1 = new DomesticCompany { Number = "РО1", FullName = "Управляющая компания", OGRN = 3333333333333, INN = 3333333333333, KPP = "333333333", IsPublic = false };
            var dc2 = new DomesticCompany { Number = "РО2", FullName = "Публичное акционерное общество Группа компаний", OGRN = 22222222, INN = 222222222222, KPP = "222222222", IsPublic = true };
            var dc3 = new DomesticCompany { Number = "РО3", FullName = "Торговая компания", OGRN = 3333333333, INN = 333333333, KPP = "333333333", IsPublic = false };

            var fc1 = new ForeignCompany { CountryCodeId = 29, Number = "ИО1", Name = "Finance Company BVI", FullName = "Файненс Кампани БиВиАй", RegistrationNumber = "123456", Address = "BVI, Ridge Rd 340" };
            var fc2 = new ForeignCompany { CountryCodeId = 57, Number = "ИО2", Name = "Cyprus Investments limited", FullName = "Сайпрус Инвестментс лимитед", RegistrationNumber = "55555555", Address = "Limassol, Odos Troyas 34" };
            var fc3 = new ForeignCompany { CountryCodeId = 39, Number = "ИО3", Name = "Cayman company", FullName = "Каймаг Лоджистик кампани", RegistrationNumber = "122222", Address = "Cayman Iceland, Bodden town 61" };

            return new List<ProjectCompany>() {
new ProjectCompany { Project = p, ModifiedDate = modifiedDate, Name = "Управляющая компания",         State = State.Domestic,       IsResident = true,   IsControlCompany = true,  IsKIKCompany = true, DomesticCompany=dc1  },
new ProjectCompany { Project = p, ModifiedDate = modifiedDate, Name = "Finance Company BVI",          State = State.Foreign,        IsResident = false,  IsControlCompany = false, IsKIKCompany = true, ForeignCompany = fc1  },
new ProjectCompany { Project = p, ModifiedDate = modifiedDate, Name = "Cyprus Investments Ltd",       State = State.Foreign,        IsResident = false,  IsControlCompany = false, IsKIKCompany = true, ForeignCompany = fc2  },
new ProjectCompany { Project = p, ModifiedDate = modifiedDate, Name = "Cayman Company",               State = State.Foreign,        IsResident = false,  IsControlCompany = false, IsKIKCompany = true, ForeignCompany = fc3  },
new ProjectCompany { Project = p, ModifiedDate = modifiedDate, Name = "International company",        State = State.ForeignLight,   IsResident = false,  IsControlCompany = false, IsKIKCompany = true, ForeignLightCompany = flc },
new ProjectCompany { Project = p, ModifiedDate = modifiedDate, Name = "Иванов И.И.",                  State = State.Individual,     IsResident = true,   IsControlCompany = true,  IsKIKCompany = true, IndividualCompany = ic  },
new ProjectCompany { Project = p, ModifiedDate = modifiedDate, Name = "ПАО ГК",                       State = State.Domestic,       IsResident = true,   IsControlCompany = true,  IsKIKCompany = true, DomesticCompany = dc2  },
new ProjectCompany { Project = p, ModifiedDate = modifiedDate, Name = "Торговая компания",            State = State.Domestic,       IsResident = true,   IsControlCompany = true,  IsKIKCompany = true, DomesticCompany = dc3  },
            };
        }

        public IList<ProjectCompanyShare> GetShares(IList<ProjectCompany> companies)
        {
            return new List<ProjectCompanyShare>()
            {
                GetShare(companies[5], companies[0], 82, false, true),
                GetShare(companies[6], companies[1], 100, false, false),
                GetShare(companies[0], companies[2], 100, false, true),
                GetShare(companies[0], companies[3], 100, false, true),
                GetShare(companies[0], companies[4], 40, true, false),
                GetShare(companies[0], companies[7], 100, false, true),
                GetShare(companies[0], companies[6], 99, false, true),
                GetShare(companies[6], companies[0], 11, false, true),
                GetShare(companies[7], companies[0], 6, false, true),
            };
        }

        private ProjectCompanyShare GetShare(ProjectCompany owner, ProjectCompany dependent, double sharePart, bool isFounder, bool isControlledBy)
        {
            var share = new ProjectCompanyShare { OwnerProjectCompany = owner, DependentProjectCompany = dependent, SharePart = sharePart, CompanyStatus = "Status", ShareType = ShareType.Direct, ControlGrounds = "фактический контроль" };
            if (isFounder)
            {
                share.IsFounder = isFounder;
                share.ControlGrounds = "учредитель";
            }
            if (isControlledBy)
            {
                share.IsControlledBy = isControlledBy;
            }

            return share;
        }

    }
}
