using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCoreHub.Models;
using WebCoreHub.Dal;

namespace WebCoreHub.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ICommonRepository<Employee> _employeeRepository;

        public EmployeesController(ICommonRepository<Employee> repository)
        {
            _employeeRepository = repository;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _employeeRepository.GetAll();
        }
    }
}
