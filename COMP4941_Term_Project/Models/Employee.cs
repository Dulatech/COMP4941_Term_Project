using System;

namespace COMP4941_Term_Project.Models
{

    public class Employee : Person
    {
        
        public Guid? EmergencyContactID { get; set; }
        public Guid? ReportRecipientID { get; set; }

        public string Role { get; set; } //[management | staff]
        public string JobTitle { get; set; }
        public string EmploymentStatus { get; set; }
        public string ReportsTo { get; set; } //ReportsTo [drop down list of JobTitles]
        public string Groups { get; set; } //multiselect list
        public string Description { get; set; }
        public string Email { get; set; }
        // Concatenated (PascalCase) controller & action names separated by "."
        public string AuthorizedActions { get; set; }
        public virtual Contact EmergencyContact { get; set; }
        public virtual Employee ReportRecipient { get; set; } //ReportingTo [FK] EmployeeEmergency [Drop Down List of Current Employees]
    }
}