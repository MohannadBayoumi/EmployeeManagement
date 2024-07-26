using EmployeeManagement.VMs.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.BL.Interfaces
{
    public interface IEmployeeBL
    {
        int AddEditEmployee(EmployeeInfoVM empInfoVM, int currentUserId);
        EmployeeInfoVM GetEmployee(int id);
        List<EmployeeInfoVM> GetEmployeesByCreatorId(ClaimsPrincipal claims); 
        void DeleteEmployee(int id);
    }
}
