using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COMP4941_Term_Project.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int ManagerID { get; set; }

        public int BranchID { get; set; }

        public string Role { get; set; }

        public enum empRole
        {
            Employee,
            Manager,
            Admin
        }

        public DateTime StartDate { get; set; }

    }
}