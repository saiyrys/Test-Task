using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Application.Dto.EmployeesDto;
using TestTask.Application.Interface.EmployeeInterface;
using TestTask.Domain.Models;

namespace TestTask.API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateEmployees([FromBody] CreateEmployeeDto employeesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var employee = await _employeeService.CreateEmployeeAsync(employeesDto);

            return Ok(employee.ToString());

        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetEmployeeByFilter([FromQuery] string type , [FromQuery] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EmployeeFilter filter = type switch
            {
                "company" => EmployeeFilter.by_company,
                "department" => EmployeeFilter.by_department,
                _ => throw new ArgumentException("Invalid filter type")
            };
            
            var employee = await _employeeService.GetEmployeesByFiltersAsync(filter, id);

            return Ok(employee);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromBody]UpdateEmployeeDto employeeDto, string id)
        {
            long employeeId = long.Parse(id);

            await _employeeService.UpdateEmployeeAsync(employeeDto, employeeId);

            return Ok(true);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployees(string id)
        {
            long employeeId = long.Parse(id);

            _employeeService.DeleteEmployeeAsync(employeeId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(true);

        }

    }
}
