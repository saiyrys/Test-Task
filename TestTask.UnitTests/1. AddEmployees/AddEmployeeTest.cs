using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Application.Dto.EmployeesDto;
using TestTask.Application.Dto.PassportsDto;
using TestTask.Application.Interface.DapperInterface;
using TestTask.Application.Interface.EmployeeInterface;
using TestTask.Application.Services.Employee;
using TestTask.Domain.IRepository.Company;
using TestTask.Domain.IRepository.Employee;
using TestTask.Domain.Models;

namespace TestTask.UnitTests._1._AddEmployees
{
    public class AddEmployeeTest
    {
        [Fact]
        public async Task AddEmployeeAsync_ShouldReturnEmployeeId_WhenCompanyExists()
        {
            var companyId = 1;
            var departmentId = 1;
            var newEmployee = new CreateEmployeeDto
            {
                Name = "Иван",
                DepartmentId = departmentId,
                CompanyId = companyId,
                Passport = new CreatePassportDto
                {
                    Type = "Паспорт",
                    Number = "123456"
                }
            };

            var companyRepoMock = new Mock<ICompanyRepository>();
            var employeeRepoMock = new Mock<IEmployeeRepository>();

            companyRepoMock.Setup(r => r.ExistAsync(companyId)).ReturnsAsync(true);
            employeeRepoMock.Setup(r => r.AddAsync(It.IsAny<Employees>(), It.IsAny<Passports>())).ReturnsAsync(42);

            var service = new EmployeesService(employeeRepoMock.Object, companyRepoMock.Object,Mock.Of<IDapperDbConnection>());


            var result = await service.CreateEmployeeAsync(newEmployee);

            Assert.Equal(42, result);
        }

        [Fact]
        public async Task AddEmployeeAsync_ShouldThrowException_WhenCompanyDoesNotExist()
        {
            var companyId = 1;
            var departmentId = 1;

            var newEmployee = new CreateEmployeeDto
            {
                Name = "Иван",
                DepartmentId = departmentId,
                CompanyId = 99,
                Passport = new CreatePassportDto
                {
                    Type = "Паспорт",
                    Number = "123456"
                }
            };

            var companyRepoMock = new Mock<ICompanyRepository>();
            var employeeRepoMock = new Mock<IEmployeeRepository>();

            companyRepoMock.Setup(r => r.ExistAsync(companyId)).ReturnsAsync(true);
            employeeRepoMock.Setup(r => r.AddAsync(It.IsAny<Employees>(), It.IsAny<Passports>())).ReturnsAsync(42);

            var service = new EmployeesService(employeeRepoMock.Object, companyRepoMock.Object, Mock.Of<IDapperDbConnection>());

            await Assert.ThrowsAsync<ArgumentException>(() => service.CreateEmployeeAsync(newEmployee));
        }
    }
}
