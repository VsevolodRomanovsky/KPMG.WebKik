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
	public class Register9DataViewModel
	{ 
		public int Id { get; set; }

		//public string ReserveTitle { get; set; }

		public Register9ViewModel Register9 { get; set; }

		//public int Register8DataTypeId { get; set; }

		public int Register9Id { get; set; }

		public string StockholderName { get; set; }

		public int CountryCodeId { get; set; }

	//	public CountryCode CountryName { get; set; }

		public int LastYearDividendPaymentYear { get; set; }

		public DateTime LastYearDividendPaymentData { get; set; }

		public double LastYearDividendSum { get; set; }

		public DateTime CurrentYearTransitionalDividendPaymentData { get; set; }

		public double CurrentYearTransitionalDividendSum { get; set; }

		public double Summary { get; set; }

		public double SummaryYear { get; set; }

		public DateTime CurrentYearDividendPaymentData { get; set; }

		public double CurrentYearDividendSum { get; set; }

		[AutomapperInitialization]
		public static void ConfigureMap(MapperConfigurationExpression cfg)
		{
			cfg.CreateMap<Models.Registers.Register9Data, Register9DataViewModel>()
				.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
				.ForMember(d => d.Register9Id, o => o.MapFrom(s => s.Register9Id))
				.ForMember(d => d.StockholderName, o => o.MapFrom(s => s.StockholderName))
				.ForMember(d => d.LastYearDividendPaymentYear, o => o.MapFrom(s => s.LastYearDividendPaymentYear))
				.ForMember(d => d.LastYearDividendPaymentData, o => o.MapFrom(s => s.LastYearDividendPaymentData))
				.ForMember(d => d.LastYearDividendSum, o => o.MapFrom(s => s.LastYearDividendSum))
				.ForMember(d => d.CurrentYearTransitionalDividendPaymentData, o => o.MapFrom(s => s.CurrentYearTransitionalDividendPaymentData))
				.ForMember(d => d.CurrentYearTransitionalDividendSum, o => o.MapFrom(s => s.CurrentYearTransitionalDividendSum))
				.ForMember(d => d.CurrentYearDividendPaymentData, o => o.MapFrom(s => s.CurrentYearDividendPaymentData))
				.ForMember(d => d.CurrentYearDividendSum, o => o.MapFrom(s => s.CurrentYearDividendSum))
				.ForMember(d => d.CountryCodeId, o => o.MapFrom(s => s.CountryCodeId))
				.ForAllOtherMembers(x => x.Ignore());

			cfg.CreateMap<Register9DataViewModel, Models.Registers.Register9Data>()
				.ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
				.ForMember(d => d.Register9Id, o => o.MapFrom(s => s.Register9Id))
				.ForMember(d => d.StockholderName, o => o.MapFrom(s => s.StockholderName))
				.ForMember(d => d.LastYearDividendPaymentYear, o => o.MapFrom(s => s.LastYearDividendPaymentYear))
				.ForMember(d => d.LastYearDividendPaymentData, o => o.MapFrom(s => s.LastYearDividendPaymentData))
				.ForMember(d => d.LastYearDividendSum, o => o.MapFrom(s => s.LastYearDividendSum))
				.ForMember(d => d.CurrentYearTransitionalDividendPaymentData, o => o.MapFrom(s => s.CurrentYearTransitionalDividendPaymentData))
				.ForMember(d => d.CurrentYearTransitionalDividendSum, o => o.MapFrom(s => s.CurrentYearTransitionalDividendSum))
				.ForMember(d => d.CurrentYearDividendPaymentData, o => o.MapFrom(s => s.CurrentYearDividendPaymentData))
				.ForMember(d => d.CurrentYearDividendSum, o => o.MapFrom(s => s.CurrentYearDividendSum))
				.ForMember(d => d.CountryCodeId, o => o.MapFrom(s => s.CountryCodeId))
				.ForAllOtherMembers(x => x.Ignore());
		}

	}
}