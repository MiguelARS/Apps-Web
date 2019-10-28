using System;
using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class Enrollment
    {
        public int EnrollmentID { get; set; }

        public int AssignmentID { get; set; }
        public Assignment Assignment { get; set; }

        public int StudentID { get; set; }
        public Student Student { get; set; }

        public Grade? Grade { get; set; }

        [Required]
        [Display(Name = "Fecha de Matrícula")]
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }

    }
}