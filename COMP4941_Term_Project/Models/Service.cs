using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace COMP4941_Term_Project.Models
{
    public class Service
    {
        [Key]
        public Guid ID { get; set; }
        public Guid BranchID { get; set; }

        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Participants { get; set; } //multi-select list of job titles
        public string Description { get; set; }
        public decimal? CostPerUnit { get; set; }
        public int? MinutePerUnit { get; set; }
        public bool? Personalized { get; set; }
        [ForeignKey("ID")]
        public virtual Branch Branch { get; set; }
    }
}