using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Entities.Enitites
{
    public class UserRoles
    {
        public int SystemUserId { get; set; }
        public int RoleId { get; set; }

        public virtual SystemUsers SystemUser { get; set; }
        public virtual Roles Role { get; set; }
    }
}
