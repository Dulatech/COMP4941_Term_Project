﻿using System;
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
        public string Title { get; set; }

        [Required]
        [RegularExpression(@"[A-Z]{1}[a-z\-]*")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [StringLength(20)]
        public string NickName { get; set; }
        public string MaidenName { get; set; }
    }
}