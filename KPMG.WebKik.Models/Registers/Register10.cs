using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models.Registers
{
    public class Register10
    {
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

        public ICollection<Register10Data> Register10Data { get; set; }
    }
}