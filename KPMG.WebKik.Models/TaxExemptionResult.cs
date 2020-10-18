using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPMG.WebKik.Models
{
    public class TaxExemptionResult
    {
        public bool IsExempted { get; set; }
        public bool IsNotEnoughData { get; set; }
    }
}
