using University.Models;
using Microsoft.EntityFrameworkCore;

namespace University.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Assignment>().ToTable("assignments");
            modelBuilder.Entity<Campus>().ToTable("campuses");
            modelBuilder.Entity<CampusCareer>().ToTable("campuscareers");
            modelBuilder.Entity<Career>().ToTable("careers");
            modelBuilder.Entity<Course>().ToTable("courses");
            modelBuilder.Entity<Enrollment>().ToTable("enrollments");
            modelBuilder.Entity<Student>().ToTable("students");            
            modelBuilder.Entity<Teacher>().ToTable("teachers");

            modelBuilder.Entity<CampusCareer>()
                .HasKey(cr => new {cr.CampusID, cr.CareerID});
            modelBuilder.Entity<CampusCareer>()
                .HasOne(cr => cr.Campus)
                .WithMany(c => c.CampusCareers)
                .HasForeignKey(cr => cr.CampusID);
            modelBuilder.Entity<CampusCareer>()
                .HasOne(cr => cr.Career)
                .WithMany(r => r.CampusCareers)
                .HasForeignKey(cr => cr.CareerID);
        }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<CampusCareer> campusCareers { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        
    }
}