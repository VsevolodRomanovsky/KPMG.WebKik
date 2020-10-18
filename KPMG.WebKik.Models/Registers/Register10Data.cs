using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPMG.WebKik.Models.Registers
{
    public class Register10Data
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Register10Id")]
        public Register10 Register10 { get; set; }
        public int Register10Id { get; set; }
        //разделы
        public int SectionId { get; set; }

        public string AssetType { get; set; }

        public string CauseDisposal { get; set; }



        public decimal? Num1 { get; set; }


        public decimal? Num2 { get; set; }


        public decimal? Num3 { get; set; }


        public decimal? Num4 { get; set; }
    }
}
