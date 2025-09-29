using Microsoft.EntityFrameworkCore;
using StudentDashboard.Models;

namespace StudentDashboard.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Check if data already exists
            if (await context.Users.AnyAsync())
            {
                return; // Database already seeded
            }

            // Seed Users
            var users = new List<User>
            {
                new User
                {
                    Email = "john.doe@student.com",
                    FirstName = "John",
                    LastName = "Doe",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                    Role = "Student",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-6),
                    LastLoginAt = DateTime.UtcNow.AddDays(-1),
                    PhoneNumber = "+1-555-0123",
                    Address = "123 Main St, Anytown, USA"
                },
                new User
                {
                    Email = "jane.smith@student.com",
                    FirstName = "Jane",
                    LastName = "Smith",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                    Role = "Student",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-4),
                    LastLoginAt = DateTime.UtcNow.AddHours(-2),
                    PhoneNumber = "+1-555-0124",
                    Address = "456 Oak Ave, Anytown, USA"
                },
                new User
                {
                    Email = "mike.johnson@student.com",
                    FirstName = "Mike",
                    LastName = "Johnson",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                    Role = "Student",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-3),
                    LastLoginAt = DateTime.UtcNow.AddDays(-3),
                    PhoneNumber = "+1-555-0125",
                    Address = "789 Pine St, Anytown, USA"
                },
                new User
                {
                    Email = "sarah.wilson@student.com",
                    FirstName = "Sarah",
                    LastName = "Wilson",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                    Role = "Student",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-2),
                    LastLoginAt = DateTime.UtcNow.AddHours(-5),
                    PhoneNumber = "+1-555-0126",
                    Address = "321 Elm St, Anytown, USA"
                },
                new User
                {
                    Email = "admin@studentdashboard.com",
                    FirstName = "Admin",
                    LastName = "User",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    Role = "Admin",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-12),
                    LastLoginAt = DateTime.UtcNow.AddHours(-1)
                }
            };

            context.Users.AddRange(users);
            await context.SaveChangesAsync();

            // Seed Students
            var students = new List<Student>
            {
                new Student
                {
                    UserId = users[0].Id,
                    StudentId = "STU2024001",
                    Group = "DD 1234",
                    Major = "Computer Science",
                    Year = "Sophomore",
                    GPA = 3.8m,
                    TotalCredits = 60,
                    CompletedCredits = 45,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-6)
                },
                new Student
                {
                    UserId = users[1].Id,
                    StudentId = "STU2024002",
                    Group = "DD 1234",
                    Major = "Computer Science",
                    Year = "Sophomore",
                    GPA = 3.6m,
                    TotalCredits = 60,
                    CompletedCredits = 42,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-4)
                },
                new Student
                {
                    UserId = users[2].Id,
                    StudentId = "STU2024003",
                    Group = "DD 1235",
                    Major = "Information Technology",
                    Year = "Junior",
                    GPA = 3.4m,
                    TotalCredits = 90,
                    CompletedCredits = 60,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-3)
                },
                new Student
                {
                    UserId = users[3].Id,
                    StudentId = "STU2024004",
                    Group = "DD 1234",
                    Major = "Computer Science",
                    Year = "Freshman",
                    GPA = 3.9m,
                    TotalCredits = 30,
                    CompletedCredits = 15,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-2)
                }
            };

            context.Students.AddRange(students);
            await context.SaveChangesAsync();

            // Seed Courses
            var courses = new List<Course>
            {
                new Course
                {
                    Name = "UX/UI Design Fundamentals",
                    Code = "UX101",
                    Description = "Learn the fundamentals of user experience and interface design. This comprehensive course covers design principles, user research, prototyping, and modern design tools.",
                    Credits = 3,
                    Format = "Online",
                    Level = "Basic",
                    Access = "Open",
                    Duration = 6,
                    Price = 299.99m,
                    StartDate = DateTime.UtcNow.AddMonths(-2),
                    EndDate = DateTime.UtcNow.AddMonths(4),
                    IsActive = true,
                    Instructor = "Prof. Sarah Johnson"
                },
                new Course
                {
                    Name = "Full Stack Web Development",
                    Code = "WEB102",
                    Description = "Master frontend and backend development technologies including HTML, CSS, JavaScript, React, Node.js, and databases.",
                    Credits = 4,
                    Format = "Online",
                    Level = "Intermediate",
                    Access = "Open",
                    Duration = 8,
                    Price = 399.99m,
                    StartDate = DateTime.UtcNow.AddMonths(-1),
                    EndDate = DateTime.UtcNow.AddMonths(7),
                    IsActive = true,
                    Instructor = "Prof. Michael Chen"
                },
                new Course
                {
                    Name = "Data Science & Analytics",
                    Code = "DATA103",
                    Description = "Learn data analysis, machine learning, and visualization using Python, R, and modern data science tools.",
                    Credits = 4,
                    Format = "Online",
                    Level = "Advanced",
                    Access = "Open",
                    Duration = 10,
                    Price = 499.99m,
                    StartDate = DateTime.UtcNow.AddDays(-15),
                    EndDate = DateTime.UtcNow.AddMonths(9),
                    IsActive = true,
                    Instructor = "Prof. Dr. Emily Rodriguez"
                },
                new Course
                {
                    Name = "Mobile App Development",
                    Code = "MOB104",
                    Description = "Build native and cross-platform mobile applications using React Native, Flutter, and native development tools.",
                    Credits = 3,
                    Format = "Online",
                    Level = "Intermediate",
                    Access = "Open",
                    Duration = 6,
                    Price = 349.99m,
                    StartDate = DateTime.UtcNow.AddDays(30),
                    EndDate = DateTime.UtcNow.AddMonths(6),
                    IsActive = true,
                    Instructor = "Prof. David Kim"
                }
            };

            context.Courses.AddRange(courses);
            await context.SaveChangesAsync();

            // Seed Enrollments
            var enrollments = new List<Enrollment>
            {
                new Enrollment
                {
                    StudentId = students[0].Id,
                    CourseId = courses[0].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-2),
                    Status = "Active"
                },
                new Enrollment
                {
                    StudentId = students[0].Id,
                    CourseId = courses[1].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-1),
                    Status = "Active"
                },
                new Enrollment
                {
                    StudentId = students[1].Id,
                    CourseId = courses[0].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-2),
                    Status = "Active"
                },
                new Enrollment
                {
                    StudentId = students[1].Id,
                    CourseId = courses[2].Id,
                    EnrollmentDate = DateTime.UtcNow.AddDays(-15),
                    Status = "Active"
                },
                new Enrollment
                {
                    StudentId = students[2].Id,
                    CourseId = courses[1].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-1),
                    Status = "Active"
                },
                new Enrollment
                {
                    StudentId = students[2].Id,
                    CourseId = courses[2].Id,
                    EnrollmentDate = DateTime.UtcNow.AddDays(-15),
                    Status = "Active"
                },
                new Enrollment
                {
                    StudentId = students[3].Id,
                    CourseId = courses[0].Id,
                    EnrollmentDate = DateTime.UtcNow.AddMonths(-2),
                    Status = "Active"
                }
            };

            context.Enrollments.AddRange(enrollments);
            await context.SaveChangesAsync();

            // Seed Assignments
            var assignments = new List<Assignment>
            {
                new Assignment
                {
                    CourseId = courses[0].Id,
                    Title = "Design Principles Research",
                    Description = "Research and analyze modern design principles used in popular applications.",
                    DueDate = DateTime.UtcNow.AddDays(7),
                    MaxPoints = 100,
                    Type = "Research",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-10)
                },
                new Assignment
                {
                    CourseId = courses[0].Id,
                    Title = "Wireframe Design",
                    Description = "Create wireframes for a mobile e-commerce application.",
                    DueDate = DateTime.UtcNow.AddDays(14),
                    MaxPoints = 150,
                    Type = "Project",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new Assignment
                {
                    CourseId = courses[1].Id,
                    Title = "React Component Library",
                    Description = "Build a reusable component library using React and TypeScript.",
                    DueDate = DateTime.UtcNow.AddDays(10),
                    MaxPoints = 200,
                    Type = "Project",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-8)
                },
                new Assignment
                {
                    CourseId = courses[1].Id,
                    Title = "API Integration",
                    Description = "Create a RESTful API using Node.js and Express.",
                    DueDate = DateTime.UtcNow.AddDays(21),
                    MaxPoints = 180,
                    Type = "Project",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-12)
                },
                new Assignment
                {
                    CourseId = courses[2].Id,
                    Title = "Data Analysis Report",
                    Description = "Analyze a dataset and create a comprehensive report with visualizations.",
                    DueDate = DateTime.UtcNow.AddDays(5),
                    MaxPoints = 120,
                    Type = "Analysis",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-7)
                }
            };

            context.Assignments.AddRange(assignments);
            await context.SaveChangesAsync();

            // Seed Tests
            var tests = new List<Test>
            {
                new Test
                {
                    CourseId = courses[0].Id,
                    Title = "Design Principles Quiz",
                    Description = "Test your knowledge of fundamental design principles.",
                    TestDate = DateTime.UtcNow.AddDays(3),
                    Duration = 30,
                    MaxPoints = 50,
                    Type = "Quiz",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new Test
                {
                    CourseId = courses[0].Id,
                    Title = "UX/UI Midterm",
                    Description = "Comprehensive midterm exam covering all topics from weeks 1-6.",
                    TestDate = DateTime.UtcNow.AddDays(21),
                    Duration = 90,
                    MaxPoints = 100,
                    Type = "Midterm",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-10)
                },
                new Test
                {
                    CourseId = courses[1].Id,
                    Title = "JavaScript Fundamentals",
                    Description = "Test your understanding of JavaScript core concepts.",
                    TestDate = DateTime.UtcNow.AddDays(7),
                    Duration = 45,
                    MaxPoints = 75,
                    Type = "Quiz",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-8)
                },
                new Test
                {
                    CourseId = courses[2].Id,
                    Title = "Python Data Analysis",
                    Description = "Practical test using Python for data analysis tasks.",
                    TestDate = DateTime.UtcNow.AddDays(12),
                    Duration = 120,
                    MaxPoints = 150,
                    Type = "Practical",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-6)
                }
            };

            context.Tests.AddRange(tests);
            await context.SaveChangesAsync();

            // Seed Schedules
            var schedules = new List<Schedule>
            {
                new Schedule
                {
                    CourseId = courses[0].Id,
                    Title = "Design Principles Lecture",
                    Description = "Introduction to fundamental design principles",
                    StartTime = DateTime.UtcNow.AddDays(1).AddHours(9),
                    EndTime = DateTime.UtcNow.AddDays(1).AddHours(10),
                    Type = "Lecture",
                    Location = "Online",
                    DayOfWeek = "Monday",
                    IsRecurring = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-10)
                },
                new Schedule
                {
                    CourseId = courses[0].Id,
                    Title = "UX Workshop",
                    Description = "Hands-on UX design workshop",
                    StartTime = DateTime.UtcNow.AddDays(3).AddHours(14),
                    EndTime = DateTime.UtcNow.AddDays(3).AddHours(16),
                    Type = "Workshop",
                    Location = "Online",
                    DayOfWeek = "Wednesday",
                    IsRecurring = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-10)
                },
                new Schedule
                {
                    CourseId = courses[1].Id,
                    Title = "React Development Lab",
                    Description = "Practical React development session",
                    StartTime = DateTime.UtcNow.AddDays(2).AddHours(10),
                    EndTime = DateTime.UtcNow.AddDays(2).AddHours(12),
                    Type = "Lab",
                    Location = "Online",
                    DayOfWeek = "Tuesday",
                    IsRecurring = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-8)
                },
                new Schedule
                {
                    CourseId = courses[2].Id,
                    Title = "Data Analysis Lecture",
                    Description = "Introduction to data analysis techniques",
                    StartTime = DateTime.UtcNow.AddDays(4).AddHours(11),
                    EndTime = DateTime.UtcNow.AddDays(4).AddHours(12),
                    Type = "Lecture",
                    Location = "Online",
                    DayOfWeek = "Thursday",
                    IsRecurring = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-6)
                }
            };

            context.Schedules.AddRange(schedules);
            await context.SaveChangesAsync();

            // Seed some sample submissions
            var submissions = new List<Submission>
            {
                new Submission
                {
                    AssignmentId = assignments[0].Id,
                    StudentId = students[0].Id,
                    Content = "Research completed on design principles...",
                    SubmittedAt = DateTime.UtcNow.AddDays(-2),
                    Status = "Graded",
                    Grade = 85,
                    Feedback = "Good research, but could use more examples.",
                    GradedAt = DateTime.UtcNow.AddDays(-1)
                },
                new Submission
                {
                    AssignmentId = assignments[2].Id,
                    StudentId = students[0].Id,
                    Content = "React component library implementation...",
                    SubmittedAt = DateTime.UtcNow.AddDays(-1),
                    Status = "Submitted"
                }
            };

            context.Submissions.AddRange(submissions);
            await context.SaveChangesAsync();

            // Seed some test results
            var testResults = new List<TestResult>
            {
                new TestResult
                {
                    TestId = tests[0].Id,
                    StudentId = students[0].Id,
                    Score = 42,
                    Percentage = 84,
                    Status = "Completed",
                    CompletedAt = DateTime.UtcNow.AddDays(-1),
                    TimeSpent = 25
                },
                new TestResult
                {
                    TestId = tests[2].Id,
                    StudentId = students[0].Id,
                    Score = 68,
                    Percentage = 91,
                    Status = "Completed",
                    CompletedAt = DateTime.UtcNow.AddDays(-2),
                    TimeSpent = 40
                }
            };

            context.TestResults.AddRange(testResults);
            await context.SaveChangesAsync();

            // Seed some grades
            var grades = new List<Grade>
            {
                new Grade
                {
                    StudentId = students[0].Id,
                    CourseId = courses[0].Id,
                    Type = "Assignment",
                    Name = "Design Principles Research",
                    Points = 85,
                    MaxPoints = 100,
                    Percentage = 85,
                    LetterGrade = "B",
                    RecordedAt = DateTime.UtcNow.AddDays(-1)
                },
                new Grade
                {
                    StudentId = students[0].Id,
                    CourseId = courses[0].Id,
                    Type = "Test",
                    Name = "Design Principles Quiz",
                    Points = 42,
                    MaxPoints = 50,
                    Percentage = 84,
                    LetterGrade = "B",
                    RecordedAt = DateTime.UtcNow.AddDays(-1)
                },
                new Grade
                {
                    StudentId = students[0].Id,
                    CourseId = courses[1].Id,
                    Type = "Test",
                    Name = "JavaScript Fundamentals",
                    Points = 68,
                    MaxPoints = 75,
                    Percentage = 91,
                    LetterGrade = "A",
                    RecordedAt = DateTime.UtcNow.AddDays(-2)
                }
            };

            context.Grades.AddRange(grades);
            await context.SaveChangesAsync();

            // Seed some rewards
            var rewards = new List<Reward>
            {
                new Reward
                {
                    StudentId = students[0].Id,
                    Type = "Marks",
                    Description = "Excellent quiz performance",
                    Points = 10,
                    EarnedAt = DateTime.UtcNow.AddDays(-1),
                    Icon = "checkmark"
                },
                new Reward
                {
                    StudentId = students[0].Id,
                    Type = "Homework",
                    Description = "On-time assignment submission",
                    Points = 5,
                    EarnedAt = DateTime.UtcNow.AddDays(-2),
                    Icon = "diamond"
                },
                new Reward
                {
                    StudentId = students[0].Id,
                    Type = "Test",
                    Description = "High test score achievement",
                    Points = 15,
                    EarnedAt = DateTime.UtcNow.AddDays(-2),
                    Icon = "trophy"
                },
                new Reward
                {
                    StudentId = students[1].Id,
                    Type = "Attendance",
                    Description = "Perfect attendance this week",
                    Points = 8,
                    EarnedAt = DateTime.UtcNow.AddDays(-1),
                    Icon = "star"
                }
            };

            context.Rewards.AddRange(rewards);
            await context.SaveChangesAsync();

            // Seed some notifications
            var notifications = new List<Notification>
            {
                new Notification
                {
                    UserId = users[0].Id,
                    Title = "Assignment Due Soon",
                    Message = "Your Design Principles Research assignment is due in 2 days.",
                    Type = "Warning",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow.AddHours(-2),
                    ActionUrl = "/courses/1/assignments/1"
                },
                new Notification
                {
                    UserId = users[0].Id,
                    Title = "Grade Posted",
                    Message = "Your JavaScript Fundamentals test grade has been posted.",
                    Type = "Success",
                    IsRead = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    ActionUrl = "/courses/2/tests/3"
                },
                new Notification
                {
                    UserId = users[1].Id,
                    Title = "New Course Material",
                    Message = "New lecture materials have been uploaded for UX/UI Design.",
                    Type = "Info",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow.AddHours(-4),
                    ActionUrl = "/courses/1"
                }
            };

            context.Notifications.AddRange(notifications);
            await context.SaveChangesAsync();

            // Seed some attendance records
            var attendances = new List<Attendance>
            {
                new Attendance
                {
                    StudentId = students[0].Id,
                    ScheduleId = schedules[0].Id,
                    Date = DateTime.UtcNow.AddDays(-7),
                    Status = "Present",
                    RecordedAt = DateTime.UtcNow.AddDays(-7)
                },
                new Attendance
                {
                    StudentId = students[0].Id,
                    ScheduleId = schedules[1].Id,
                    Date = DateTime.UtcNow.AddDays(-5),
                    Status = "Present",
                    RecordedAt = DateTime.UtcNow.AddDays(-5)
                },
                new Attendance
                {
                    StudentId = students[1].Id,
                    ScheduleId = schedules[0].Id,
                    Date = DateTime.UtcNow.AddDays(-7),
                    Status = "Present",
                    RecordedAt = DateTime.UtcNow.AddDays(-7)
                },
                new Attendance
                {
                    StudentId = students[1].Id,
                    ScheduleId = schedules[1].Id,
                    Date = DateTime.UtcNow.AddDays(-5),
                    Status = "Late",
                    Notes = "Technical difficulties",
                    RecordedAt = DateTime.UtcNow.AddDays(-5)
                }
            };

            context.Attendances.AddRange(attendances);
            await context.SaveChangesAsync();
        }
    }
}
