using System.ComponentModel.DataAnnotations;

namespace StudentDashboard.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Student";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Avatar { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        // Navigation properties
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }

    public class Student
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string? Group { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        public string? Major { get; set; }
        public string? Year { get; set; }
        public decimal GPA { get; set; } = 0;
        public int TotalCredits { get; set; } = 0;
        public int CompletedCredits { get; set; } = 0;

        // Navigation properties
        public User User { get; set; } = null!;
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
        public ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
        public ICollection<Reward> Rewards { get; set; } = new List<Reward>();
    }

    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int Credits { get; set; } = 3;
        public string Format { get; set; } = "Online";
        public string Level { get; set; } = "Basic";
        public string Access { get; set; } = "Open";
        public int Duration { get; set; } = 6; // months
        public decimal Price { get; set; } = 0;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Instructor { get; set; }

        // Navigation properties
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
        public ICollection<Test> Tests { get; set; } = new List<Test>();
        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
        public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
    }

    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Active"; // Active, Completed, Dropped
        public decimal? FinalGrade { get; set; }
        public DateTime? CompletionDate { get; set; }

        // Navigation properties
        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }

    public class Assignment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public int MaxPoints { get; set; } = 100;
        public string Type { get; set; } = "Homework"; // Homework, Project, Essay
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Course Course { get; set; } = null!;
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }

    public class Submission
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int StudentId { get; set; }
        public string? Content { get; set; }
        public string? FilePath { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Submitted"; // Submitted, Graded, Returned
        public decimal? Grade { get; set; }
        public string? Feedback { get; set; }
        public DateTime? GradedAt { get; set; }

        // Navigation properties
        public Assignment Assignment { get; set; } = null!;
        public Student Student { get; set; } = null!;
    }

    public class Test
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime TestDate { get; set; }
        public int Duration { get; set; } = 60; // minutes
        public int MaxPoints { get; set; } = 100;
        public string Type { get; set; } = "Quiz"; // Quiz, Midterm, Final
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Course Course { get; set; } = null!;
        public ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
    }

    public class TestResult
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int StudentId { get; set; }
        public decimal Score { get; set; }
        public decimal Percentage { get; set; }
        public string Status { get; set; } = "Completed"; // Completed, Failed, Pending
        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
        public int TimeSpent { get; set; } = 0; // minutes

        // Navigation properties
        public Test Test { get; set; } = null!;
        public Student Student { get; set; } = null!;
    }

    public class Attendance
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ScheduleId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = "Present"; // Present, Absent, Late, Excused
        public string? Notes { get; set; }
        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Student Student { get; set; } = null!;
        public Schedule Schedule { get; set; } = null!;
    }

    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string Type { get; set; } = "Assignment"; // Assignment, Test, Final
        public string Name { get; set; } = string.Empty;
        public decimal Points { get; set; }
        public decimal MaxPoints { get; set; }
        public decimal Percentage { get; set; }
        public string LetterGrade { get; set; } = string.Empty;
        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Student Student { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }

    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Message { get; set; }
        public string Type { get; set; } = "Info"; // Info, Warning, Success, Error
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? ActionUrl { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
    }

    public class Reward
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Type { get; set; } = string.Empty; // Marks, Homework, Test, Attendance
        public string Description { get; set; } = string.Empty;
        public int Points { get; set; }
        public DateTime EarnedAt { get; set; } = DateTime.UtcNow;
        public string? Icon { get; set; }

        // Navigation properties
        public Student Student { get; set; } = null!;
    }

    public class Schedule
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Type { get; set; } = "Lecture"; // Lecture, Workshop, Lab
        public string Location { get; set; } = "Online";
        public string DayOfWeek { get; set; } = string.Empty;
        public bool IsRecurring { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Course Course { get; set; } = null!;
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
