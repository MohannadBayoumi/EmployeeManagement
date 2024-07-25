using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Entities.Enitites
{
    public class SystemUsers
    {
        public SystemUsers()
        {
            UserRoles = new HashSet<UserRoles>();
        }
        public int Id { get; set; }
        [MaxLength(250)]
        public string FullName { get; set; }
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
