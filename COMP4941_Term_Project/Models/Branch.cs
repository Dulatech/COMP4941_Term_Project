using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COMP4941_Term_Project.Models
{
    public class Branch
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? ParentBranchID { get; set; }
    }
}