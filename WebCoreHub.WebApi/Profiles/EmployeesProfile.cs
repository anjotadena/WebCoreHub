using AutoMapper;
using WebCoreHub.Models;
using WebCoreHub.WebApi.DTO;

namespace WebCoreHub.WebApi.Profiles
{
    public class EmployeesProfile : Profile
    {
        public EmployeesProfile()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<NewEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDetailDto, Employee>();
        }
    }
}
