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
	public class Register11DataViewModel
	{ 
		public int Id { get; set; }

		public Register11ViewModel Register11 { get; set; }


		public int Register11Id { get; set; }

		/// <summary>
		/// Тип данных 11 регистра:
		/// 1 Ценные бумаги
		/// 2 Имущественные права - доли
		/// 3 Имущественные права - Паи
		/// 4 Иные имущественные права
		/// </summary>
		public int Register11DataTypeId { get; set; }

		/// <summary>
		/// Вид реализованного финансового актива 
		/// </summary>
		public string RealizedFinancialAsset { get; set; }

		/// <summary>
		/// наименование покупателя
		/// </summary>
		public string BuyerName { get; set; }

		/// <summary>
		/// № договора реализации
		/// </summary>
		public string ContractOfRealizationNo { get; set; }

		/// <summary>
		/// дата перехода права собственности
		/// </summary>
		public DateTime? PropertyRightTransitionData { get; set; }

		/// <summary>
		/// эмитент
		/// </summary>
		public string Issuer { get; set; }

		/// <summary>
		/// №и дата  выпуска
		/// </summary>
		public string DateOfIssueNumber { get; set; }

		/// <summary>
		/// Доход, полученный КИК от реализации финансового актива - всего
		/// </summary>
		public double IncomeFromRealizationOfAssetSummary { get; set; }

		/// <summary>
		/// Доход, полученный КИК от реализации финансового актива - в т.ч. цена реализации финансового актива 
		/// </summary>
		public double IncomeFromRealizationOfAssetSellPrice { get; set; }

		/// <summary>
		/// Доход, полученный КИК от реализации финансового актива - в т.ч. иной доход (процентный, купонный)
		/// </summary>
		public double IncomeFromRealizationOfAssetOthers { get; set; }

		/// <summary>
		/// Рыночная стоимость
		/// </summary>
		public double MarketValue { get; set; }

		/// <summary>
		/// Стоимость по данным бухгалтерского учета на дату перехода права собственности (выбытия) - всего
		/// </summary>
		public double CostForTransitionOfPropertyRightDateSummary { get; set; }

		/// <summary>
		/// Стоимость по данным бухгалтерского учета на дату перехода права собственности (выбытия) - в т.ч. цена приобретения
		/// </summary>
		public double CostForTransitionOfPropertyRightDateAcquisitionPrice { get; set; }

		/// <summary>
		/// Стоимость по данным бухгалтерского учета на дату перехода права собственности (выбытия) - в т.ч. сумма накопленной переоценки на дату перехода права собственности (выбытия) 
		///(сальдо переоценок)** - всего
		/// </summary>
		public double CostForTransitionOfPropertyRightDateRevaluationSummary { get; set; }

		/// <summary>
		/// Стоимость по данным бухгалтерского учета на дату перехода права собственности (выбытия) - в т.ч. сумма накопленной переоценки на дату перехода права собственности (выбытия) 
		///(сальдо переоценок)** - в т.ч. за отчетный финансовый год
		/// </summary>
		public double CostForTransitionOfPropertyRightDateRevaluationForCurrentYear { get; set; }

		/// <summary>
		/// Доход, исключаемый из прибыли (убытка) КИК
		/// </summary>
		public double IncomeExcludedFromProfitLoss { get; set; }

		/// <summary>
		/// Расход, исключаемый из прибыли (убытка) КИК
		/// </summary>
		public double ExpenseExcludedFromProfitLoss{ get; set; }

	[AutomapperInitialization]
		public static void ConfigureMap(MapperConfigurationExpression cfg)
		{
			cfg.CreateMap<Models.Registers.Register11Data, Register11DataViewModel>()
				.ForMember(d => d.Register11, o => o.Ignore())
				.ForMember(d => d.IncomeExcludedFromProfitLoss, o => o.Ignore())
				.ForMember(d => d.ExpenseExcludedFromProfitLoss, o => o.Ignore());

			cfg.CreateMap<Register11DataViewModel, Models.Registers.Register11Data>()
				.ForMember(d => d.Register11, o => o.Ignore());
		}
	}
}