using EmployeeManagement.VMs.VMs;
using EmployeeManagementAPI;
using EmployeeManagementSystem.BL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.APIs.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthorizationBL _authorizationBL;
        public AuthorizeController(IAuthorizationBL authorizationBL)
        {
            _authorizationBL = authorizationBL;
        }

        [HttpPost(Name = "Login")]
        public UserVM Login([FromBody] UserLoginVM loginVM)
        {
            try
            {
                return _authorizationBL.Login(loginVM.Username, loginVM.Password);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
