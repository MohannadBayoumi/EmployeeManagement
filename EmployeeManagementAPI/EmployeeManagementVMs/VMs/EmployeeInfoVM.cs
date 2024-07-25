using EmployeeManagementSystem.VMs.Enums;
using System.Runtime.Serialization;

namespace EmployeeManagement.VMs.VMs
{
    [DataContract()]
    public class EmployeeInfoVM
    {
        [DataMember(Name = "id")]
        public int? Id { get; set; }
        [DataMember(Name = "fullName")]
        public string FullName { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "phoneNumber")]
        public string PhoneNumber { get; set; }
        [DataMember(Name = "graduateDegree")]
        public GraduateDegree GraduateDegree { get; set; }
        [DataMember(Name = "image")]
        public string Image { get; set; }
    }
}
