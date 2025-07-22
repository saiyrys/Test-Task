using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Application.Dto.EmployeesDto;
using TestTask.Domain.Models;

namespace TestTask.Application.Interface.EmployeeInterface
{
    public interface IEmployeeService
    {
        Task<long> CreateEmployeeAsync(CreateEmployeeDto createEmployees);

        Task<IEnumerable<GetEmployeeDto>> GetEmployeesByFiltersAsync(EmployeeFilter filters, int id);

        Task<bool> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployee, long id);

        Task<bool> DeleteEmployeeAsync(long id);

    }
}
