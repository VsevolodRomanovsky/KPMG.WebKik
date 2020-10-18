using KPMG.Webkik.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPMG.WebKik.Models.Registers
{
    public class RegisterListItem
    {
        public int Id { get; set; }
        public string Name
        {
            get
            {
                return Type.GetDescription();
            }
        }

        public bool IsFilled { get; set; }
        public RegisterType Type { get; set; }
        public string Link
        {
            get
            {
                return $"{Type.ToString().ToLower()}/{Id}";
            }
        }
    }
}
