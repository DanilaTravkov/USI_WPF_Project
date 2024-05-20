using Microsoft.EntityFrameworkCore;
using WPFTutorial.Model;

namespace WPFTutorial.DB
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Teacher> Teachers {  get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Datafile.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<User>("User")
                .HasValue<Student>("Student")
                .HasValue<Teacher>("Teacher");

            // Configure one-to-many relationship
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CourseId); // Assuming you have a foreign key in Student class

            base.OnModelCreating(modelBuilder);
        }
    }
}
