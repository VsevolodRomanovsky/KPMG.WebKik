using System.Collections.Generic;

namespace KPMG.WebKik.Models
{
    public class Role : IEntity<int>
    {
        public const string Administrator = "Администратор";
        public const string Employee = "Сотрудник";

        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
