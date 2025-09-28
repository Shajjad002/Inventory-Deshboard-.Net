using System.ComponentModel.DataAnnotations;

namespace StudentDashboard.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = "Student";
        public string? Avatar { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class Student
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        [Required]
        public string StudentNumber { get; set; } = string.Empty;
        public string? Group { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
        public ICollection<Reward> Rewards { get; set; } = new List<Reward>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }

    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Duration { get; set; } // in months
        public string Level { get; set; } = "Basic";
        public string Format { get; set; } = "Online";
        public string Access { get; set; } = "Open";
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
        public ICollection<Test> Tests { get; set; } = new List<Test>();
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }

    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public string Status { get; set; } = "Active";
        public decimal? FinalGrade { get; set; }
    }

    public class Assignment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public int MaxPoints { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }

    public class Test
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime TestDate { get; set; }
        public int Duration { get; set; } // in minutes
        public int MaxPoints { get; set; }
        public string Status { get; set; } = "Scheduled";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }

    public class Attendance
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Status { get; set; } = "Present"; // Present, Absent, Late, Excused
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        public int? AssignmentId { get; set; }
        public Assignment? Assignment { get; set; }
        public int? TestId { get; set; }
        public Test? Test { get; set; }
        public decimal Points { get; set; }
        public decimal MaxPoints { get; set; }
        public decimal Percentage => MaxPoints > 0 ? (Points / MaxPoints) * 100 : 0;
        public string? Comments { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }

    public class Reward
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        [Required]
        public string Type { get; set; } = string.Empty; // Marks, Homework, Test, Attendance, etc.
        public string? Description { get; set; }
        public int Points { get; set; }
        public DateTime AwardedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }

    public class Notification
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; } = null!;
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = "Info"; // Info, Warning, Success, Error
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReadAt { get; set; }
    }

    public class Schedule
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        public string Type { get; set; } = string.Empty; // Lecture, Workshop, Lab, etc.
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Location { get; set; }
        public string Status { get; set; } = "Scheduled";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
