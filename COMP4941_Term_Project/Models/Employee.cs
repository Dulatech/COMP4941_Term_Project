using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMP4941_Term_Project.Models
{
    [Table("Employee")]
    public class Employee : Person
    {
        
        public Guid? EmergencyContactID { get; set; }
        public Guid? ReportRecipientID { get; set; }
        [Required]
        public string Role { get; set; } //[management | staff]
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string EmploymentStatus { get; set; }
        public string ReportsTo { get; set; } //ReportsTo [drop down list of JobTitles]
        public string Groups { get; set; } //multiselect list
        [MaxLength(50, ErrorMessage = "The Discription is limited to 50 charcters.")]
        public string Description { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        // Concatenated (PascalCase) controller & action names separated by "."
        public string AuthorizedActions { get; set; }

        public virtual Contact EmergencyContact { get; set; }
        public virtual Employee ReportRecipient { get; set; } //ReportingTo [FK] EmployeeEmergency [Drop Down List of Current Employees]
    }
}