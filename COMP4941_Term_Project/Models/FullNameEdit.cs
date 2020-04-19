using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace COMP4941_Term_Project.Models
{
    public class FullNameEdit
    {
        public Guid fnID { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string NickName { get; set; }
        public string MaidenName { get; set; }

        public FullName UpdateFullName(FullName name)
        {
            System.Diagnostics.Debug.WriteLine("Name Before: " + name.FirstName + " " + name.LastName);
            name.Title = this.Title;
            name.FirstName = this.FirstName;
            name.MiddleName = this.MiddleName;
            name.LastName = this.LastName;
            name.NickName = this.NickName;
            name.MaidenName = this.MaidenName;
            System.Diagnostics.Debug.WriteLine("Name After: " + name.FirstName + " " + name.LastName);

            return name;
        }
    }
}