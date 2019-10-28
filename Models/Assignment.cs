using System.Collections.Generic;

namespace University.Models
{
    public class Assignment
    {
        public int AssignmentID { get; set; }

        public int CampusID { get; set; }
        public Campus Campus { get; set; }

        public int CourseID { get; set; }
        public Course Course { get; set; }

        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}