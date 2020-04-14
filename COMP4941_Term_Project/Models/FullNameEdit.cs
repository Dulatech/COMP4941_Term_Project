﻿using System;
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

        public string Title { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string NickName { get; set; }
        public string MaidenName { get; set; }
    }
}