using AutoMapper.Configuration;
using KPMG.WebKik.Models;
using KPMG.WebKik.Models.Registers;
using KPMG.WebKik.Web.Automapper;
using KPMG.WebKik.Web.Controllers.ProjectCompanies;
using KPMG.WebKik.Web.Controllers.ProjectCompanyShare;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KPMG.WebKik.Web.Controllers.Register
{
	[DataContract(IsReference = true)]
	[JsonObject(IsReference = false)]
	public class Register8Dto
    {
        public int Id { get; set; }
        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompanyViewModel OwnerProjectCompany { get; set; }
        public RegisterType Type { get; set; }
        public int Year { get; set; }
        public string Currency { get; set; }

		public ICollection<Register8DataDto> Register8Data { get; set; }

		[AutomapperInitialization]
		public static void ConfigureMap(MapperConfigurationExpression cfg)
		{
			cfg.CreateMap<Register8Dto, Register8>()
			.ForMember(m => m.Register8Data, o => o.MapFrom(s => s.Register8Data));

			cfg.CreateMap<Register8, Register8Dto>()
			.ForMember(m => m.Register8Data, o => o.MapFrom(s => s.Register8Data));
		}
	}
}