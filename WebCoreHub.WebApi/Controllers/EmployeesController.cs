﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCoreHub.Models;
using WebCoreHub.Dal;
using Microsoft.AspNetCore.Authorization;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Employee,HR")]
        public ActionResult<IEnumerable<Employee>> Get()
        {
            var employees = _employeeRepository.GetAll();

            if (employees.Count <= 0)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Employee,HR")]
        public ActionResult<Employee> GetDetails(int id)
        {
            var employee = _employeeRepository.GetDetails(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Employee,HR")]
        public ActionResult Create(Employee employee)
        {
            _employeeRepository.Insert(employee);

            var result = _employeeRepository.SaveChanges();

            if (result > 0)
            {
                // actionName = The name of the action to use for generating the URL
                // routeValues = The route data to use for generating the URL
                // value = The content value to format in the entity body
                return CreatedAtAction("GetDetails", new { id = employee.EmployeeId }, employee);
            }

            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Employee,HR")]
        public ActionResult Update(Employee employee)
        {
            _employeeRepository.Update(employee);

            var result = _employeeRepository.SaveChanges();

            if (result > 0)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "HR")]
        public ActionResult<Employee> Delete(int id)
        {
            var employee = _employeeRepository.GetDetails(id);

            if (employee == null)
            {
                return NotFound();
            }

            _employeeRepository.Delete(employee);

            _employeeRepository.SaveChanges();
        
            return NoContent();
        }
    }
}
