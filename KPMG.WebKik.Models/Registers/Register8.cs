using KPMG.WebKik.Models.ProjectCompanies;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KPMG.WebKik.Models.Registers
{
	public class Register8 
	{
		public Register8()
		{
			Register8Data = new HashSet<Register8Data>();
		}

		[Key]
		public int Id { get; set; }

		[Required]
		public int Year { get; set; }

		public RegisterType Type { get; set; }

		public int OwnerProjectCompanyId { get; set; }

		[ForeignKey("OwnerProjectCompanyId")]
		public ProjectCompany OwnerProjectCompany { get; set; }

		[Required]
		public string Currency { get; set; }

		public ICollection<Register8Data> Register8Data { get; set; }
	}
}
