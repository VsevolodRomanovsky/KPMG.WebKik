using System;
using System.Collections.Generic;
using KPMG.WebKik.Models.ProjectCompanies;

namespace KPMG.WebKik.Models
{
    public class Project : IEntity<int>
    {
        public Project()
        {
            Users = new HashSet<User>();
            ProjectCompanies = new HashSet<ProjectCompany>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? CreationDate { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<ProjectCompany> ProjectCompanies { get; set; }
    }
}
