using Microsoft.EntityFrameworkCore;
using WPFTutorial.Model;

namespace WPFTutorial.DB
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Director> Directors { get; set; } // the dbset is named DirectorS although there can only be one director account
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<CourseApplication> CourseApplications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Datafile.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring Teacher to Course relationship
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Courses)
                .WithOne(c => c.Teacher)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring Student to Course relationship
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring CourseApplication relationships
            modelBuilder.Entity<CourseApplication>()
                .HasOne(ca => ca.Student)
                .WithMany(s => s.CourseApplications)
                .HasForeignKey(ca => ca.StudentId);

            modelBuilder.Entity<CourseApplication>()
                .HasOne(ca => ca.Course)
                .WithMany(c => c.CourseApplications)
                .HasForeignKey(ca => ca.CourseId);

            // Configuring Student to Exam relationship
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Exam)
                .WithMany(e => e.Students)
                .HasForeignKey(s => s.ExamId)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
    }
}
