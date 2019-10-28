using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace University.Models
{
    public class Student
    {
        public int ID { get; set; }
        
        [Required(ErrorMessage= "El apellido es obligatorio")]
        [StringLength(40, MinimumLength = 10)]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(40, MinimumLength = 10, ErrorMessage = "El tamaño del Nombre debe ser de 10 a 40")]
        [Display(Name = "Nombres")]
        public string FirstMidName { get; set; }

        public int CareerID { get; set; }       
        [JsonIgnore] 
        public Career Career { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}