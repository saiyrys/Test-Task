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
using TestTask.Application.Services.Employee;
using TestTask.Domain.IRepository.Company;
using TestTask.Domain.IRepository.Employee;
using TestTask.Domain.Models;

namespace TestTask.UnitTests._2._DeleteEmployees
{
    public class DeleteEmployeeTest
    {
        [Fact]
        public async Task DeleteEmployeeAsync_ShouldReturnTrue_IfUserExists()
        {
            var employeeRepoMock = new Mock<IEmployeeRepository>();

            employeeRepoMock
                .Setup(r => r.DeleteAsync(10))
                .ReturnsAsync(true);

            var service = new EmployeesService(employeeRepoMock.Object, Mock.Of<ICompanyRepository>(), Mock.Of<IDapperDbConnection>());

            await service.DeleteEmployeeAsync(10);

            employeeRepoMock.Verify(r => r.DeleteAsync(10));
        }
    }
}
