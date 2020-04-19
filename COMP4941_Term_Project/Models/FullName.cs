using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace COMP4941_Term_Project.Models
{
    public class FullName
    {
        public Guid ID { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z]{1,3}\.", ErrorMessage = "Title must be 1 to 3 characters followed by a period.")]
        public string Title { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z\-]*", ErrorMessage = "First name must only contain letters and hyphens.")]

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [RegularExpression(@"[a-zA-Z\-]*", ErrorMessage = "Middle name must only contain letters and hyphens.")]
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z\-]*", ErrorMessage = "Last name must only contain letters and hyphens.")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [StringLength(20)]
        [RegularExpression(@"[a-zA-Z\-]*", ErrorMessage = "Nickname must only contain letters and hyphens.")]
        public string NickName { get; set; }

        [RegularExpression(@"[a-zA-Z\-]*", ErrorMessage = "Maiden name must only contain letters and hyphens.")]
        public string MaidenName { get; set; }
    }
}