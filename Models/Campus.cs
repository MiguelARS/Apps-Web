using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Campus
    {
        public int CampusID { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        public ICollection<CampusCareer> CampusCareers { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
}