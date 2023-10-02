using swingvy.Enums;
using System;
using System.Collections.Generic;

namespace swingvy.Models
{
    public class EmployeeViewModel
    {
        public string Name { get; set; }
        public Department Depart { get; set; }
        public Position Ption { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
    }
}
