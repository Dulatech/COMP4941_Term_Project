﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace COMP4941_Term_Project.Models
{
    [Table("Customer")]
    public class Customer : Person
    {
        public Customer()
        {
            Contacts = new HashSet<Person>();
        }
        public virtual ICollection<Person> Contacts { get; set; }
    }
}