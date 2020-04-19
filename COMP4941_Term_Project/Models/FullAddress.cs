﻿using System;
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
        public string RoomNo { get; set; }
        public string POBox { get; set; }
        public string Unit { get; set; }

        [Range(1,200)]
        [Required]
        public string Floor { get; set; }
        public string Wing { get; set; }
        public string Building { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string Country { get; set; }

        [DisplayName("Postal Code")]
        [Required]
        public string PostalCode { get; set; }
        public string Cell { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public Guid PersonID { get; set; }
    }
}