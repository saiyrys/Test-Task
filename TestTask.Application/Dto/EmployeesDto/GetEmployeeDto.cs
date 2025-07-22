using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Application.Dto.DepartmentsDto;
using TestTask.Application.Dto.PassportsDto;
using TestTask.Domain.Models;

namespace TestTask.Application.Dto.EmployeesDto
{
    public class GetEmployeeDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int CompanyId { get; set; }
        public GetPassportDto Passport { get; set; }
        public GetDepartmentsDto Department { get; set; }
    }
}
