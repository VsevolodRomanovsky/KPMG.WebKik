using KPMG.WebKik.Models.Directories;
using KPMG.WebKik.Models.ProjectCompanies;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPMG.WebKik.Models.Registers
{
	public class Register11Data
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("Register11Id")]
		public Register11 Register11 { get; set; }

		/// <summary>
		/// Id 11 регистра
		/// </summary>
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
	}
}
