using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace COMP4941_Term_Project.Models
{
    public class Person
    {
        [Key]
        public Guid ID { get; set; }
        public Guid? BranchID { get; set; }


        [ForeignKey("ID")]
        public virtual Branch Branch { get; set; }
        [ForeignKey("ID")]
        public virtual FullName Name { get; set; }
        [ForeignKey("ID")]
        public virtual FullAddress HomeAddress { get; set; }
        [ForeignKey("ID")]

        public virtual FullAddress WorkAddress { get; set; }
        public virtual Byte[] Picture { get; set; }
    }
}