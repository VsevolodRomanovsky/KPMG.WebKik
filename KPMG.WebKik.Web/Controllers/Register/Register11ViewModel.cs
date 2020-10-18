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
	public class Register11ViewModel
    {
        public int Id { get; set; }
        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompanyViewModel OwnerProjectCompany { get; set; }
        public RegisterType Type { get; set; }
        public int Year { get; set; }
        public string Currency { get; set; }

		public DateTime DecisionOfLiquidationData { get; set; }

		public DateTime CompletionOfLiquidationData { get; set; }

		public ICollection<Register11DataViewModel> Register11Data { get; set; }

		public Register11DataViewModel Summary1 { get; set; }

		public Register11DataViewModel Summary2 { get; set; }

		public Register11DataViewModel Summary3 { get; set; }

		public Register11DataViewModel Summary4 { get; set; }

		public Register11DataViewModel Summary { get; set; }

		[AutomapperInitialization]
		public static void ConfigureMap(MapperConfigurationExpression cfg)
		{
			cfg.CreateMap<Register11ViewModel, Register11>()
			.ForMember(m => m.Register11Data, o => o.MapFrom(s => s.Register11Data));

			cfg.CreateMap<Register11, Register11ViewModel>()
			.ForMember(m => m.Register11Data, o => o.MapFrom(s => s.Register11Data))
			.ForMember(m => m.Summary, c => c.Ignore())
			.ForMember(m => m.Summary1, c => c.Ignore())
			.ForMember(m => m.Summary2, c => c.Ignore())
			.ForMember(m => m.Summary3, c => c.Ignore())
			.ForMember(m => m.Summary4, c => c.Ignore());
		}
	}
}