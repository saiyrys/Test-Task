using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Domain.Models
{
    public class Employees
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Phone { get; init; }
        public int Company_Id { get; init; }
        public int Department_Id { get; init; }
        public Passports Passports { get; set; }
        public Departments Departments { get; set; }
    }
}
