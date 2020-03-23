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
        [Key]
        public Guid ID { get; set; }
        [DisplayName("Room No")]
        public string RoomNo { get; set; }
        public string POBox { get; set; }
        public string Unit { get; set; }
        public string Floor { get; set; }
        public string Wing { get; set; }
        public string Building { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
        public string Cell { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        [ForeignKey("ID")]
        public virtual Branch Branch { get; set; }
    }
}