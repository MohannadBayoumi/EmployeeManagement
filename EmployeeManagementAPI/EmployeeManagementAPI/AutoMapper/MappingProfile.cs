using AutoMapper;
using EmployeeManagement.VMs.VMs;
using EmployeeManagementSystem.Entities.Enitites;

namespace EmployeeManagement.APIs.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeInfoVM, Employees>();
            CreateMap<Employees, EmployeeInfoVM>();
        }
    }
}
