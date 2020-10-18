using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using KPMG.WebKik.Models;

namespace KPMG.WebKik.Data.EntityConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserLogin)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute { IsUnique = true }));

            HasMany(u => u.Projects)
                .WithMany(p => p.Users)
                .Map(m =>
                {                    
                    m.MapLeftKey("UserId");
                    m.MapRightKey("ProjectId");
                    m.ToTable("UserProjects");                    
                });

            HasRequired(x => x.Role).WithMany(x => x.Users).HasForeignKey(x => x.RoleId);
        }
    }
}