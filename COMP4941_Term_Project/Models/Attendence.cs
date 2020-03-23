using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COMP4941_Term_Project.Models
{
    public class Attendance
    {
        [Key]
        public int ID { get; set; }
        public Guid EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
        public DateTime Timestamp { get; set; }
        public string Activity { get; set; }

    }
}