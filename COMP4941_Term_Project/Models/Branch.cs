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
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$", ErrorMessage = "Invalid character.")]
        public string Name { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Fax { get; set; }
        [Required]
        public string Email { get; set; }
        public string Website { get; set; }
    
        public virtual Branch ParentBranch { get; set; }
        public virtual ICollection<Person> People { get; set; }

        public virtual ICollection<Branch> SubBranches { get; set; }
    }
}