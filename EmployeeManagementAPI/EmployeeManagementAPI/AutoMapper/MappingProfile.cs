using AutoMapper;
using EmployeeManagement.VMs.VMs;
using EmployeeManagementSystem.Entities.Enitites;

namespace EmployeeManagement.APIs.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeInfoVM, Employees>().ForMember(e => e.GraduateDegree, opt => opt.MapFrom(s => (int)s.GraduateDegree)).ReverseMap();
        }
    }
}
