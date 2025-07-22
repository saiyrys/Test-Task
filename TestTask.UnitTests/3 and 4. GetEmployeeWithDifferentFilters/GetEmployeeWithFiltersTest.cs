using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Application.Dto.EmployeesDto;
using TestTask.Application.Interface.DapperInterface;
using TestTask.Application.Services.Employee;
using TestTask.Domain.IRepository.Company;
using TestTask.Domain.IRepository.Employee;
using TestTask.Domain.Models;

namespace TestTask.UnitTests._3._GetEmployeeWithDifferentFilters
{
    public class GetEmployeeWithFiltersTest
    {
        [Fact]
        public async Task GetEmployeesByFiltersAsync_ShouldReturnEmployees_ByCompany()
        {
            var companyId = 1;

            var employees = new List<Employees>
            {
                new Employees
                {
                    Name = "Иван",
                    Surname = "Иванов",
                    Phone = "123456",
                    Company_Id = companyId,
                    Passports = null,
                    Departments = null
                }
            };

            var employeeRepoMock = new Mock<IEmployeeRepository>();
            employeeRepoMock
                .Setup(r => r.GetEmployeesByFilterAsync(EmployeeFilter.by_company, companyId))
                .ReturnsAsync(employees);

            var service = new EmployeesService(employeeRepoMock.Object, Mock.Of<ICompanyRepository>(), Mock.Of<IDapperDbConnection>());

            var result = (await service.GetEmployeesByFiltersAsync(EmployeeFilter.by_company, companyId)).ToList();

            Assert.Single(result);
            Assert.Equal("Иван", result[0].Name);
            Assert.Equal(companyId, result[0].CompanyId);
        }

        [Fact]
        public async Task GetEmployeesByFiltersAsync_ShouldReturnEmployees_ByDepartment()
        {
            var employees = new List<Employees>
            {
                new Employees
                {
                    Name = "Федор",
                    Surname = "Федоров",
                    Phone = "123456",
                    Company_Id = 2,
                    Passports = new Passports { Type = "ID", Number = "9876" },
                    Departments = new Departments { Name = "HR", Phone = "112233" }
                }
            };

            var employeeRepoMock = new Mock<IEmployeeRepository>();
            employeeRepoMock
                .Setup(r => r.GetEmployeesByFilterAsync(EmployeeFilter.by_department, 2))
                .ReturnsAsync(employees);

            var service = new EmployeesService(employeeRepoMock.Object, Mock.Of<ICompanyRepository>(), Mock.Of<IDapperDbConnection>());

            var result = (await service.GetEmployeesByFiltersAsync(EmployeeFilter.by_department, 2)).ToList();

            Assert.Single(result);
            Assert.Equal("Федор", result[0].Name);
            Assert.Equal("HR", result[0].Department.Name);
            Assert.Equal("112233", result[0].Department.Phone);
        }
    }
}
