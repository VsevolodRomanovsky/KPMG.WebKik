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
	public class Register9ViewModel
    {
        public int Id { get; set; }
        public int OwnerProjectCompanyId { get; set; }
        public ProjectCompanyViewModel OwnerProjectCompany { get; set; }
        public RegisterType Type { get; set; }
        public int Year { get; set; }
        public string Currency { get; set; }
		public double Summary { get; set; }

		public double SummaryYear { get; set; }

		public double CurrentYearDividendSumAggr { get; set; }

		public double CurrentYearTransitionalDividendSumAggr { get; set; }

		public double LastYearDividendSumAggr { get; set; }

		public ICollection<Register9DataViewModel> Register9Data { get; set; }

		[AutomapperInitialization]
		public static void ConfigureMap(MapperConfigurationExpression cfg)
		{
			cfg.CreateMap<Register9ViewModel, Register9>()
			.ForMember(m => m.Register9Data, o => o.MapFrom(s => s.Register9Data));

			cfg.CreateMap<Register9, Register9ViewModel>()
			.ForMember(m => m.Register9Data, o => o.MapFrom(s => s.Register9Data))
			.ForMember(m => m.Summary, c => c.Ignore())
			.ForMember(m => m.SummaryYear, c => c.Ignore())
			.ForMember(m => m.LastYearDividendSumAggr, c => c.Ignore())
			.ForMember(m => m.CurrentYearTransitionalDividendSumAggr, c => c.Ignore())
			.ForMember(m => m.CurrentYearDividendSumAggr, c => c.Ignore());
		}
	}
}