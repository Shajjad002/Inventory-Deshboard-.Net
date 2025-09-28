using Microsoft.EntityFrameworkCore;
using StudentDashboard.Models;

namespace StudentDashboard.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Student configuration
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.StudentNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Group).HasMaxLength(20);
                entity.HasIndex(e => e.StudentNumber).IsUnique();
                entity.HasOne(e => e.User).WithOne().HasForeignKey<Student>(e => e.UserId);
            });

            // Course configuration
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            // Enrollment configuration
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Student).WithMany(s => s.Enrollments).HasForeignKey(e => e.StudentId);
                entity.HasOne(e => e.Course).WithMany(c => c.Enrollments).HasForeignKey(e => e.CourseId);
                entity.HasIndex(e => new { e.StudentId, e.CourseId }).IsUnique();
            });

            // Assignment configuration
            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.HasOne(e => e.Course).WithMany(c => c.Assignments).HasForeignKey(e => e.CourseId);
            });

            // Test configuration
            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.HasOne(e => e.Course).WithMany(c => c.Tests).HasForeignKey(e => e.CourseId);
            });

            // Attendance configuration
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Student).WithMany(s => s.Attendances).HasForeignKey(e => e.StudentId);
                entity.HasOne(e => e.Schedule).WithMany(s => s.Attendances).HasForeignKey(e => e.ScheduleId);
            });

            // Grade configuration
            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Student).WithMany(s => s.Grades).HasForeignKey(e => e.StudentId);
                entity.HasOne(e => e.Assignment).WithMany(a => a.Grades).HasForeignKey(e => e.AssignmentId);
                entity.HasOne(e => e.Test).WithMany(t => t.Grades).HasForeignKey(e => e.TestId);
            });

            // Reward configuration
            modelBuilder.Entity<Reward>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Description).HasMaxLength(200);
                entity.HasOne(e => e.Student).WithMany(s => s.Rewards).HasForeignKey(e => e.StudentId);
            });

            // Notification configuration
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Message).IsRequired().HasMaxLength(1000);
                entity.HasOne(e => e.Student).WithMany(s => s.Notifications).HasForeignKey(e => e.StudentId);
            });

            // Schedule configuration
            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Type).IsRequired().HasMaxLength(50);
                entity.HasOne(e => e.Course).WithMany(c => c.Schedules).HasForeignKey(e => e.CourseId);
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "phillip.vetrov@student.com", FirstName = "Phillip", LastName = "Vetrov", Role = "Student", CreatedAt = DateTime.UtcNow },
                new User { Id = 2, Email = "andrew.vetrov@student.com", FirstName = "Andrew", LastName = "Vetrov", Role = "Student", CreatedAt = DateTime.UtcNow },
                new User { Id = 3, Email = "david.boreanaz@student.com", FirstName = "David", LastName = "Boreanaz", Role = "Student", CreatedAt = DateTime.UtcNow },
                new User { Id = 4, Email = "william.pitt@student.com", FirstName = "William", LastName = "Pitt", Role = "Student", CreatedAt = DateTime.UtcNow },
                new User { Id = 5, Email = "armand.hammer@student.com", FirstName = "Armand", LastName = "Hammer", Role = "Student", CreatedAt = DateTime.UtcNow }
            );

            // Seed Students
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, UserId = 1, StudentNumber = "STU001", Group = "DD 1234", CreatedAt = DateTime.UtcNow },
                new Student { Id = 2, UserId = 2, StudentNumber = "STU002", Group = "DD 1234", CreatedAt = DateTime.UtcNow },
                new Student { Id = 3, UserId = 3, StudentNumber = "STU003", Group = "DD 1234", CreatedAt = DateTime.UtcNow },
                new Student { Id = 4, UserId = 4, StudentNumber = "STU004", Group = "DD 1234", CreatedAt = DateTime.UtcNow },
                new Student { Id = 5, UserId = 5, StudentNumber = "STU005", Group = "DD 1234", CreatedAt = DateTime.UtcNow }
            );

            // Seed Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "UX/UI Design", Code = "UX101", Description = "Introduction to User Experience and User Interface Design", Duration = 6, Level = "Basic", Format = "Online", CreatedAt = DateTime.UtcNow },
                new Course { Id = 2, Name = "Web Development", Code = "WEB201", Description = "Full-stack web development course", Duration = 8, Level = "Intermediate", Format = "Online", CreatedAt = DateTime.UtcNow },
                new Course { Id = 3, Name = "Data Science", Code = "DS301", Description = "Data analysis and machine learning", Duration = 10, Level = "Advanced", Format = "Hybrid", CreatedAt = DateTime.UtcNow }
            );
        }
    }
}
