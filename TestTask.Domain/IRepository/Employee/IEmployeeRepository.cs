using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Domain.Models;

namespace TestTask.Domain.IRepository.Employee
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employees>> GetEmployeesByFilterAsync(EmployeeFilter filter, int id);

        Task<long> AddAsync(Employees employees, Passports passports);
        
        Task<bool> DeleteAsync(long id);
    }
}
