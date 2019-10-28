using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace University.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 5)]
        [Display(Name = "Nombre de Curso")]
        public string Title { get; set; }

        [Required]
        [Range(3, 5, ErrorMessage = "El creditaje debe estar entre 3 a 5")]
        public int Credits { get; set; }
        public int CareerID { get; set; }    
        [JsonIgnore]    
        public Career Career { get; set; }

        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        
    }
}   