using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace COMP4941_Term_Project.Models
{
    [Table("Contact")]
    public class Contact : Person
    {
        public string RelationPrimary { get; set; } //Dropdown List
        public string RelationSecondary { get; set; }
        public string Description { get; set; } //Key Value Pairs
        public virtual Person Person { get; set; }
    }
}