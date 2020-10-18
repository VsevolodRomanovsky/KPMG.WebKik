using KPMG.WebKik.Models.ProjectCompanies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPMG.WebKik.Models.Registers
{
	public class Register11
	{
		public Register11()
		{
			Register11Data = new HashSet<Register11Data>();
		}

		[Key]
		public int Id { get; set; }

		[Required]
		public int Year { get; set; }

		public RegisterType Type { get; set; }

		public int OwnerProjectCompanyId { get; set; }

		[ForeignKey("OwnerProjectCompanyId")]
		public ProjectCompany OwnerProjectCompany { get; set; }

		public DateTime? DecisionOfLiquidationData { get; set; }

		public DateTime? CompletionOfLiquidationData { get; set; }

		[Required]
		public string Currency { get; set; }

		public ICollection<Register11Data> Register11Data { get; set; }
	}
}
