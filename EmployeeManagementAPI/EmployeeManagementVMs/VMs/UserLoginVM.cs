using System.Runtime.Serialization;

namespace EmployeeManagement.VMs.VMs
{
    [DataContract()]
    public class UserLoginVM
    {
        [DataMember(Name = "username")]
        public string Username { get; set; }
        [DataMember(Name = "password")]
        public string Password { get; set; }
    }
}
