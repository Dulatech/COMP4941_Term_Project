using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace COMP4941_Term_Project.Models
{
   
    public class Customer : Person
    {
        public Customer()
        {
            Contacts = new HashSet<Person>();
        }
        public virtual ICollection<Person> Contacts { get; set; }
    }
}