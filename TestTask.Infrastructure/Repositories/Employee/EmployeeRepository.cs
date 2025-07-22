using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Application.Interface.DapperInterface;
using TestTask.Domain.IRepository.Employee;
using TestTask.Domain.Models;

namespace TestTask.Infrastructure.Repositories.Employee
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDapperDbConnection _dapperConnection;
        public EmployeeRepository(IDapperDbConnection dapperConnection)
        {
            _dapperConnection = dapperConnection;
        }

        public async Task<long> AddAsync(Employees employees, Passports passport)
        {
            using var connection = _dapperConnection.CreateConnection();

            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                const string employeeSql = @" INSERT INTO employees (Id, Name, Surname, Phone, Company_Id, Department_id)
                    VALUES(@Id, @Name, @Surname, @Phone, @Company_Id, @Department_Id)
                    RETURNING Id;";

                long employeeId = connection.ExecuteScalar<long>(employeeSql, employees, transaction: transaction);

                passport.EmployeeId = employeeId;

                const string passportSql = @"INSERT INTO passports(employee_id, type, number)
                    VALUES(@EmployeeId, @Type, @Number);";

                await connection.ExecuteAsync(passportSql, passport, transaction: transaction);

                transaction.Commit();

                return employeeId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<IEnumerable<Employees>> GetEmployeesByFilterAsync(EmployeeFilter filter, int id)
        {
            using var connection = _dapperConnection.CreateConnection();

            string column = filter switch
            {
                EmployeeFilter.by_company => "company_id",
                EmployeeFilter.by_department => "department_id",
                _ => null
            };

            string sql = $@"SELECT employees.*,
                    passports.type, passports.number,   
                    departments.name, departments.phone
                FROM employees 
                JOIN passports ON employees.id = passports.employee_id
                LEFT JOIN departments ON employees.department_id = departments.id
                WHERE employees.{column} = @id";

            var result = await connection.QueryAsync<Employees, Passports, Departments, Employees>(
                sql,
                (employees, passport, department) =>
                {
                    employees.Passports = passport;
                    employees.Departments = department;
                    return employees;
                }, new { Id = id }, splitOn: "type,name");

            return result;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            using var connection = _dapperConnection.CreateConnection();

            try
            {
                const string employeeSql = @"DELETE FROM employees WHERE Id = @id";
                await connection.ExecuteAsync(employeeSql, new { id });

                return true;
            }
            catch
            {
                throw;
            }
            
        }

    }
}
