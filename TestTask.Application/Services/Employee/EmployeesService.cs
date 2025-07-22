using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Application.Dto.DepartmentsDto;
using TestTask.Application.Dto.EmployeesDto;
using TestTask.Application.Dto.PassportsDto;
using TestTask.Application.Interface.DapperInterface;
using TestTask.Application.Interface.EmployeeInterface;
using TestTask.Domain.IRepository.Company;
using TestTask.Domain.IRepository.Employee;
using TestTask.Domain.Models;

namespace TestTask.Application.Services.Employee
{
    public class EmployeesService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IDapperDbConnection _dapperConnection;
        public EmployeesService(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository,
            IDapperDbConnection dapperConnection)
        {
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
            _dapperConnection = dapperConnection;
        }

        public async Task<long> CreateEmployeeAsync(CreateEmployeeDto employeesDto)
        {
            var passport = new Passports
            {
                Type = employeesDto.Passport.Type,
                Number = employeesDto.Passport.Number
            };


            if (!await _companyRepository.ExistAsync(employeesDto.CompanyId))
                throw new ArgumentException($"Компания с данным ID:{employeesDto.CompanyId} - не найдена");

            var employee = new Employees {
                Id = await GenerateUniqueId(),
                Name = employeesDto.Name,
                Surname = employeesDto.Surname,
                Phone = employeesDto.Phone,
                Company_Id = employeesDto.CompanyId,
                Department_Id = employeesDto.DepartmentId,
                Passports = passport
            };

            var employeeId = await _employeeRepository.AddAsync(employee, passport);

            return employeeId;
        }

        public async Task<IEnumerable<GetEmployeeDto>> GetEmployeesByFiltersAsync(EmployeeFilter filter, int id)
        {
            var employees = await _employeeRepository.GetEmployeesByFilterAsync(filter, id);

            var result = employees.Select(e => new GetEmployeeDto
            {
                Name = e.Name,
                Surname = e.Surname,
                Phone = e.Phone,
                CompanyId = e.Company_Id,
                Passport = e.Passports == null ? null : new GetPassportDto
                {
                    Type = e.Passports.Type,
                    Number = e.Passports.Number
                },
                Department = e.Departments == null ? null : new GetDepartmentsDto
                {
                    Name = e.Departments.Name,
                    Phone = e.Departments.Phone
                }
            });

            return result;
        }
        public async Task<bool> UpdateEmployeeAsync(UpdateEmployeeDto dto, long id)
        {
            var employeeUpdates = new List<string>();
            var passportUpdates = new List<string>();

            var employeeParams = new DynamicParameters();
            var passportParams = new DynamicParameters();

            employeeParams.Add("Id", id);
            passportParams.Add("Id", id);

            var propertyMap = new Dictionary<string, string>
            {
                { "CompanyId", "Company_Id" },
                { "DepartmentId", "Department_Id" }
            };

            foreach (var prop in dto.GetType().GetProperties())
            {
                var value = prop.GetValue(dto);
                if (value == null)
                    continue;

                if (prop.Name == "Passport" && value is UpdatePassportDto passport)
                {
                    foreach (var pProp in passport.GetType().GetProperties())
                    {
                        var pVal = pProp.GetValue(passport);
                        if (pVal != null)
                        {
                            var dbName = pProp.Name;
                            passportUpdates.Add($"{dbName} = @{dbName}");
                            passportParams.Add(dbName, pVal);
                        }
                    }
                }
                else if (prop.PropertyType == typeof(string) || prop.PropertyType.IsValueType)
                {
                    var dbName = propertyMap.ContainsKey(prop.Name) ? propertyMap[prop.Name] : prop.Name;
                    employeeUpdates.Add($"{dbName} = @{dbName}");
                    employeeParams.Add(dbName, value);
                }
            }

            if (employeeUpdates.Count == 0 && passportUpdates.Count == 0)
                return false;

            using var connection = _dapperConnection.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            if (employeeUpdates.Count > 0)
            {
                var sqlEmployee = $"UPDATE employees SET {string.Join(", ", employeeUpdates)} WHERE id = @Id";
                await connection.ExecuteAsync(sqlEmployee, employeeParams, transaction);
            }

            if (passportUpdates.Count > 0)
            {
                var sqlPassport = $"UPDATE passports SET {string.Join(", ", passportUpdates)} WHERE employee_id = @Id";
                await connection.ExecuteAsync(sqlPassport, passportParams, transaction);
            }

            transaction.Commit();
            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(long id)
        {
            if (!await _employeeRepository.DeleteAsync(id))
            {
                throw new ArgumentException("Ошибка удаления пользователя");
            }

            return true;
        }

        private async Task<long> GenerateUniqueId()
        {
            Random random = new Random();
            return ((long)(uint)random.Next(int.MinValue, int.MaxValue)) << 32 | (uint)random.Next(int.MinValue, int.MaxValue);
        }
    }
}
