using System.Collections.Generic;

namespace KPMG.WebKik.Models
{
    public class User: IEntity<int>
    {
        public User()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }

        public string UserLogin { get; set; }

        public string DisplayName { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        public int RoleId { get; set; }

        public Role Role { get; set; }

        public bool IsDisabled { get; set; }
    }
}
