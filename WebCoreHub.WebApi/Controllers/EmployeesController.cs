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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Employee>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get()
        {
            var employees = _employeeRepository.GetAll();

            if (employees.Count <= 0)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDetails(int id)
        {
            var employee = _employeeRepository.GetDetails(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }
    }
}
