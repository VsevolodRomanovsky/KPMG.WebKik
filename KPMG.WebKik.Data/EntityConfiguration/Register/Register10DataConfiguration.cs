using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KPMG.WebKik.Models.Registers;

namespace KPMG.WebKik.Data.EntityConfiguration.Register
{
    public class Register10DataConfiguration : EntityTypeConfiguration<Register10Data>
    {
        public Register10DataConfiguration()
        {
            ToTable("Register10Data");
            HasKey(r => r.Id);
            HasRequired(x => x.Register10).WithMany(x => x.Register10Data).HasForeignKey(x => x.Register10Id);
            Property(x => x.Num1).HasPrecision(16, 5);
            Property(x => x.Num2).HasPrecision(16, 5);
            Property(x => x.Num3).HasPrecision(16, 5);
            Property(x => x.Num4).HasPrecision(16, 5);
        }
    }
}
