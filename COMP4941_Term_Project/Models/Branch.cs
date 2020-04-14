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

        [Index(IsUnique = true), MaxLength(32)]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,50}$", ErrorMessage = "Invalid charcter.")]
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
    
        public virtual Branch ParentBranch { get; set; }
        public virtual ICollection<Person> People { get; set; }

        public virtual ICollection<Branch> SubBranches { get; set; }
    }
}