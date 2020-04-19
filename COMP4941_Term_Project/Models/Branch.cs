using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace COMP4941_Term_Project.Models
{
    public class Branch
    {
        public Branch()
        {
            People = new HashSet<Person>();
            SubBranches = new HashSet<Branch>();
        }
       
        public Guid ID { get; set; }
        
        public Guid? ParentID { get; set; }
        [Required]
        [Index(IsUnique = true), MaxLength(32)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,32}$", ErrorMessage = "Must contain only letters, spaces and hyphens and between 1-32 characters.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9''-'\s]{1,100}$", ErrorMessage = "Must contain only letters, spaces and hyphens and between 1-50 characters.")]
        public string Street { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$", ErrorMessage = "Must contain only letters, spaces and hyphens and between 1-50 characters.")]
        public string City { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z]*", ErrorMessage = "Must contain only letters.")]
        public string Province { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$", ErrorMessage = "Must contain only letters, spaces and hyphens and between 1-50 characters.")]
        public string Country { get; set; }
        [Required]
        [RegularExpression(@"^[\w\d\w\d\w\d]{5,6}$", ErrorMessage = "Must be a valid postal code.")]
        public string PostalCode { get; set; }
        [Required]
        [RegularExpression(@"[\d\-]*")]
        public string Phone { get; set; }
        [RegularExpression(@"[\d\-]*")]
        public string Fax { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z0-9]+", ErrorMessage = "Must be a valid email address.")]
        public string Email { get; set; }
        [RegularExpression(@"^((https?|ftp|smtp):\/\/)?(www.)?[a-z0-9]+(.[a-z]+)+\/?", ErrorMessage = "Must be a valid website address.")]
        public string Website { get; set; }
    
        public virtual Branch ParentBranch { get; set; }
        public virtual ICollection<Person> People { get; set; }

        public virtual ICollection<Branch> SubBranches { get; set; }
    }
}