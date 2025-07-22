using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Application.Dto.DepartmentsDto;
using TestTask.Application.Dto.PassportsDto;

namespace TestTask.Application.Dto.EmployeesDto
{
    public class UpdateEmployeeDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public int? CompanyId { get; set; }
        public int? DepartmentId { get; set; }
        public UpdatePassportDto? Passport { get; set; }
    }
}
