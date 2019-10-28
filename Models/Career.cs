using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Career
    {
        public int CareerID { get; set; }

        [Required(ErrorMessage = "Es obligatorio colocar el nombre")]
        [StringLength(40, MinimumLength = 10, ErrorMessage = "El nombre tiene tamaño incorrecto")]
        public String Name { get; set; }

        public ICollection<CampusCareer> CampusCareers { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
}