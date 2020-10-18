using KPMG.WebKik.Models.ProjectCompanies;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPMG.WebKik.Models.Registers
{
	public class Register8Data
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("Register8Id")]
		public Register8 Register8 { get; set; }

		/// <summary>
		/// Тип вопроса о резерве (от 1 до 12)
		/// </summary>
		[Required]
		public int Register8DataTypeId { get; set; }

		public int Register8Id { get; set; }

		public double? ExpensesFormationOfReserve { get; set; }

		public double? ExpensesReducedOfReserve { get; set; }

		public double? IncomeFromRecoveryOfReserve { get; set; }
	}
}
