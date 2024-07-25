using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Entities.Enitites
{
    public class Roles
    {
        public Roles()
        {
            UserRoles = new HashSet<UserRoles>();
        }
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
