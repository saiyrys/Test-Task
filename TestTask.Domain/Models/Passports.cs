using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Domain.Models
{
    public class Passports
    {
        public long EmployeeId { get; set; }
        public string Type { get; init; }

        public string Number { get; init; }
    }
}
