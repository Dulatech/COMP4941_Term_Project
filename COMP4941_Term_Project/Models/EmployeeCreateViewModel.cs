using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace COMP4941_Term_Project.Models
{
    public class EmployeeCreateViewModel
    {
        public Employee Employee { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
    }
}