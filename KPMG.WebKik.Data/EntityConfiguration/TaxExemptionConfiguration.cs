using KPMG.WebKik.Models.ProjectCompanies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPMG.WebKik.Data.EntityConfiguration
{
    public class TaxExemptionConfiguration : EntityTypeConfiguration<TaxExemption>
    {
        public TaxExemptionConfiguration()
        {
            ToTable("ProjectTaxExemption");
            HasKey(f => f.Id);
            Property(f => f.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(f => f.Year).IsRequired();
            Property(f => f.Rationalities).IsRequired();
        }
    }
}
