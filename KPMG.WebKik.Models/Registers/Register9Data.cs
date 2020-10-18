using KPMG.WebKik.Models.Directories;
using KPMG.WebKik.Models.ProjectCompanies;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPMG.WebKik.Models.Registers
{
	public class Register9Data
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("Register9Id")]
		public Register9 Register9 { get; set; }

		public int Register9Id { get; set; }

		public string StockholderName { get; set; }

		public int CountryCodeId { get; set; }

		[ForeignKey("CountryCodeId")]
		public CountryCode CountryName { get; set; }

		public int LastYearDividendPaymentYear { get; set; }

		public DateTime? LastYearDividendPaymentData { get; set; }

		public double LastYearDividendSum { get; set; }

		public DateTime? CurrentYearTransitionalDividendPaymentData { get; set; }

		public double CurrentYearTransitionalDividendSum { get; set; }

		public DateTime? CurrentYearDividendPaymentData { get; set; }

		public double CurrentYearDividendSum { get; set; }
	}
}
