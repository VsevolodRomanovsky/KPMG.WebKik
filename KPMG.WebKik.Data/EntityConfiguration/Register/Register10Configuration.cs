using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Data.EntityConfiguration.Register
{
    class Register10Configuration : EntityTypeConfiguration<Register10>
    {
        public Register10Configuration()
        {
            ToTable("Registers10");
            HasKey(r => r.Id);
            Property(r => r.Year).IsRequired();
            Property(r => r.Type);
            Property(r => r.Currency).IsRequired();
            HasRequired(x => x.OwnerProjectCompany).WithMany(x => x.Registers10).HasForeignKey(x => x.OwnerProjectCompanyId);
        }
    }
}
