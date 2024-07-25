using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Entities.Enitites
{
    public class Employees
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string FullName { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        public int GraduateDegree { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedById { get; set; }
        public int ModifiedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual SystemUsers CreatedBy { get; set; }
        public virtual SystemUsers ModifiedBy { get; set; }
    }
}
