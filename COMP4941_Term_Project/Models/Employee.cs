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
        [RegularExpression(@"[a-zA-z]{1,15}", ErrorMessage = "Must be letters only between 1 and 15 characters.")]
        public string Role { get; set; } //[management | staff]
        [Required]
        [RegularExpression(@"[a-zA-z]{1,50}", ErrorMessage = "Must be letters only between 1 and 50 characters.")]
        public string JobTitle { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-z]{1,20}", ErrorMessage = "Must be letters only between 1 and 20 characters.")]
        public string EmploymentStatus { get; set; }
        [RegularExpression(@"[a-zA-z]{1,50}", ErrorMessage = "Must be letters only between 1 and 50 characters.")]
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