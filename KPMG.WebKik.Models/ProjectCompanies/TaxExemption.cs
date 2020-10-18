using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPMG.WebKik.Models.ProjectCompanies
{
    public class TaxExemption : IEntity<int>
    {
        public int Id { get; set; }

        public RationalyType[] Rationaly
        {
            get
            {
                return (Rationalities ?? "")
                    .Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    .Select(code => (RationalyType)Enum.Parse(typeof(RationalyType), code))
                    .ToArray();
            }

            set
            {
                Rationalities = string.Join("|", value.Select(x => x.ToString()));
            }
        }

        public string Rationalities { get; set; }

        public bool? Result { get; set; }       
            

        public int Year { get; set; }


        public int OwnerProjectCompanyId { get; set; }
        //public ProjectCompany OwnerProjectCompany { get; set; }
        public int DependentProjectCompanyId { get; set; }
        //public ProjectCompany DependentProjectCompany { get; set; }
    }
}
