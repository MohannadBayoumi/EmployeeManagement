using System.Runtime.Serialization;

namespace EmployeeManagement.VMs.VMs
{
    [DataContract()]
    public class UserVM
    {
        [DataMember(Name = "userId")]
        public int? UserId { get; set; }
        [DataMember(Name = "username")]
        public string Username { get; set; }
        [DataMember(Name = "role")]
        public string Role { get; set; }
        [DataMember(Name = "token")]
        public string Token { get; set; }
    }
}
