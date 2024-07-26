using AutoMapper;
using EmployeeManagement.VMs.VMs;
using EmployeeManagementSystem.BL.Interfaces;
using EmployeeManagementSystem.Entities.Enitites;
using EmployeeManagementSystemDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.BL.Implementations
{
    public class EmployeeBL : IEmployeeBL
    {
        private readonly TeleDBContext _dbContext;
        private readonly IMapper _mapper;
        public EmployeeBL(TeleDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        public int AddEditEmployee(EmployeeInfoVM empInfoVM, int userId)
        {
            var emp = _dbContext.Employees.Where(e => e.Id == empInfoVM.Id).FirstOrDefault() ?? new Employees();

            emp.Email = empInfoVM.Email;
            emp.PhoneNumber = empInfoVM.PhoneNumber;
            emp.FullName = empInfoVM.FullName;
            emp.GraduateDegree = (int)empInfoVM.GraduateDegree;
            emp.Image = empInfoVM.Image;

            // In case of editting
            if (emp.Id != 0)
            {
                emp.ModifiedById = userId;
                emp.ModifiedOn = DateTime.Now;
            }
            else
            {
                emp.CreatedById = userId;
                emp.CreatedOn = DateTime.Now;
                _dbContext.Employees.Add(emp);
            }
            _dbContext.SaveChanges();

            return emp.Id;
        }

        public EmployeeInfoVM GetEmployee(int id)
        {
            var emp = _dbContext.Employees.Where(e => e.Id == id).FirstOrDefault();
            if (emp == null)
            {
                throw new Exception("Employee not found");
            }
            return _mapper.Map<EmployeeInfoVM>(emp);
        }

        public List<EmployeeInfoVM> GetEmployeesByCreatorId(ClaimsPrincipal claims)
        {
            var roles = claims.FindFirst(ClaimTypes.Role).Value;
            var currentUserId = int.Parse(claims.FindFirst(ClaimTypes.NameIdentifier).Value);
            var isAdmin = roles.ToLower().Contains("admin");

            List<Employees> emps = _dbContext.Employees.Where(e => isAdmin || e.CreatedById == currentUserId).ToList();
            return _mapper.Map<List<Employees>, List<EmployeeInfoVM>>(emps);
        }

        public void DeleteEmployee(int id)
        {
            var emp = _dbContext.Employees.Where(e => e.Id == id).FirstOrDefault();
            emp.IsDeleted = true;
            _dbContext.SaveChanges();
            return;
        }
    }
}
