﻿using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace COMP4941_Term_Project.Models
{

    public class Contact : Person
    {
        public string RelationPrimary { get; set; } //Dropdown List
        public string RelationSecondary { get; set; }
        public string Description { get; set; } //Key Value Pairs
    }
}