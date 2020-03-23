﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace COMP4941_Term_Project.Models
{
    public class Employee : Person
    {
        public Employee()
        {
            AttendanceHistory = new HashSet<Attendance>();
        }

        public Guid? EmergencyContactID { get; set; }
        public Guid? ReportRecipientID { get; set; }

        public string Role { get; set; } //[management | staff]
        public string JobTitle { get; set; }
        public string EmploymentStatus { get; set; }
        public string ReportsTo { get; set; } //ReportsTo [drop down list of JobTitles]
        public string Groups { get; set; } //multiselect list
        public string Description { get; set; }
        public string Password { get; set; }
        [ForeignKey("ID")]
        public virtual Contact EmergencyContact { get; set; }
        [ForeignKey("ID")]
        public virtual Employee ReportRecipient { get; set; } //ReportingTo [FK] EmployeeEmergency [Drop Down List of Current Employees]

        public virtual ICollection<Attendance> AttendanceHistory { get; set; }
    }
}