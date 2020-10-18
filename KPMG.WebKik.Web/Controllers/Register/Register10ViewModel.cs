using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;
using KPMG.WebKik.Web.Controllers.ProjectCompanyShare;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace KPMG.WebKik.Web.Controllers.Register
{
    [DataContract(IsReference = true)]
    [JsonObject(IsReference = false)]
    public class Register10ViewModel : IEntity<int>
    {
        public int Id { get; set; }
        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompanyViewModel OwnerProjectCompany { get; set; }
        public RegisterType Type { get; set; }
        public Year Year { get; set; }
        public string Currency { get; set; }

        public ICollection<Register10DataViewModel> Register10Data { get; set; }

        [AutomapperInitialization]
        public static void ConfigureMap(MapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Register10, Register10ViewModel>();
            cfg.CreateMap<Register10ViewModel, Register10>()
            .ForMember(r => r.OwnerProjectCompany, c => c.Ignore())
            .ForMember(r => r.Register10Data, c => c.Ignore());
        }
    }
}