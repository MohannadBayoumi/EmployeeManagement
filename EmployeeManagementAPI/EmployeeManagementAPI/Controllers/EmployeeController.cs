using EmployeeManagement.VMs.VMs;
using EmployeeManagementSystem.BL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManagement.APIs.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeBL _employeeBL;

        public EmployeeController(IEmployeeBL employeeBL)
        {
            _employeeBL = employeeBL;
        }

        [HttpPost(Name = "AddEditEmployee")]
        //[Authorize(Roles = "Admin, Normal")]
        public int AddEditEmployee([FromBody] EmployeeInfoVM? empInfoVM)
        {
            try
            {
                return _employeeBL.AddEditEmployee(empInfoVM, int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet(Name = "GetEmployee")]
        //[Authorize(Roles = "Admin, Normal")]
        public EmployeeInfoVM GetEmployee(int id)
        {
            try
            {
                return _employeeBL.GetEmployee(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet(Name = "GetAllEmployees")]
        //[Authorize(Roles = "Admin, Normal")]
        public List<EmployeeInfoVM> GetAllEmployees()
        {
            try
            {
                return _employeeBL.GetEmployeesByCreatorId(int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete(Name = "DeleteEmployee")]
        //[Authorize(Roles = "Admin, Normal")]
        public void DeleteEmployee(int id)
        {
            try
            {
                _employeeBL.DeleteEmployee(id);
                return;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
