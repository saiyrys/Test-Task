﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Domain.Models
{
    public class Departments
    {
        public int Id { get; private set; }
        public string Name { get; init; }

        public string Phone { get; init; }
    }
}
