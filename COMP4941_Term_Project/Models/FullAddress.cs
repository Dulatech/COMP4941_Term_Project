using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace COMP4941_Term_Project.Models
{
    public class FullAddress
    {
        public Guid ID { get; set; }
        [DisplayName("Room No")]
        [Range(1, 999)]
        public string RoomNo { get; set; }
        public string POBox { get; set; }
        public string Unit { get; set; }

        [Range(1,200)]
        [Required]
        public string Floor { get; set; }
        [RegularExpression(@"[a-zA-Z0-9\s]*", ErrorMessage = "Must contain only letters, numbers, and spaces.")]
        public string Wing { get; set; }
        [RegularExpression(@"[a-zA-Z0-9\s]*", ErrorMessage = "Must contain only letters, numbers, and spaces.")]
        public string Building { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z0-9\s]*", ErrorMessage = "Must contain only letters, numbers, and spaces.")]
        public string Street { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z\s]*", ErrorMessage = "Must contain only letters and spaces.")]
        public string City { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z]*", ErrorMessage = "Must contain only letters.")]
        public string Province { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z]*", ErrorMessage = "Must contain only letters.")]
        public string Country { get; set; }

        [DisplayName("Postal Code")]
        [Required]
        [RegularExpression(@"^[\w\d\w\d\w\d]{5,6}$", ErrorMessage = "Must be a valid postal code.")]
        public string PostalCode { get; set; }
        public string Cell { get; set; }

        [RegularExpression(@"[0-9\-]*", ErrorMessage = "Must be a valid phone number.")]

        public string Phone { get; set; }

        [RegularExpression(@"[0-9\-]*", ErrorMessage = "Must be a valid phone number.")]
        public string Fax { get; set; }

        [RegularExpression(@"[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z0-9]+", ErrorMessage = "Must be a valid email address.")]

        public string Email { get; set; }
        public virtual Branch Branch { get; set; }
    }
}