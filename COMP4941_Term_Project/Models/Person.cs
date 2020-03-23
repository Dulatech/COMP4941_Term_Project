using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace COMP4941_Term_Project.Models
{
    public class Person
    {
        public Guid ID { get; set; }

        public Guid? BranchID { get; set; }



        public virtual Branch Branch { get; set; }
        public virtual FullName Name { get; set; }
        public virtual FullAddress HomeAddress { get; set; }
        public virtual FullAddress WorkAddress { get; set; }
        public virtual Byte[] Picture { get; set; }
    }
}