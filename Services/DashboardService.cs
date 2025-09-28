using Microsoft.EntityFrameworkCore;
using StudentDashboard.Data;
using StudentDashboard.Models;

namespace StudentDashboard.Services
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardDataAsync(int userId);
        Task<List<GpaDataPoint>> GetGpaDataAsync(int studentId, string period = "Week");
        Task<List<AttendanceDataPoint>> GetAttendanceDataAsync(int studentId, string period = "Month");
        Task<HomeworkData> GetHomeworkDataAsync(int studentId);
        Task<TestData> GetTestDataAsync(int studentId);
        Task<CalendarData> GetCalendarDataAsync(int studentId, DateTime? date = null);
        Task<RatingData> GetRatingDataAsync(int studentId);
        Task<CourseInfo> GetCourseInfoAsync(int studentId);
        Task<List<RewardItem>> GetRewardsDataAsync(int studentId, string period = "Today");
        Task<List<LeaderboardEntry>> GetLeaderboardDataAsync(int studentId, string filter = "Group");
    }

    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync(int userId)
        {
            var student = await _context.Students
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (student == null)
            {
                throw new ArgumentException("Student not found");
            }

            return new DashboardViewModel
            {
                UserProfile = new UserProfile
                {
                    Name = $"{student.User.FirstName} {student.User.LastName}",
                    Group = student.Group ?? "DD 1234",
                    Avatar = student.User.Avatar ?? "/images/avatar.png",
                    Stars = await GetTotalRewardsAsync(student.Id),
                    Trophies = await GetTotalRewardsAsync(student.Id),
                    Diamonds = await GetTotalRewardsAsync(student.Id),
                    Notifications = await GetUnreadNotificationsCountAsync(userId)
                },
                ProgressGpa = new ProgressGpaData
                {
                    DataPoints = await GetGpaDataAsync(student.Id, "Week")
                },
                Attendance = new AttendanceData
                {
                    DataPoints = await GetAttendanceDataAsync(student.Id, "Month")
                },
                Homework = await GetHomeworkDataAsync(student.Id),
                Test = await GetTestDataAsync(student.Id),
                Calendar = await GetCalendarDataAsync(student.Id),
                Rating = await GetRatingDataAsync(student.Id),
                CourseInfo = await GetCourseInfoAsync(student.Id),
                Rewards = new RewardsData
                {
                    Rewards = await GetRewardsDataAsync(student.Id, "Today")
                },
                Leaderboard = new LeaderboardData
                {
                    Entries = await GetLeaderboardDataAsync(student.Id, "Group")
                }
            };
        }

        public async Task<List<GpaDataPoint>> GetGpaDataAsync(int studentId, string period = "Week")
        {
            var endDate = DateTime.UtcNow;
            var startDate = period switch
            {
                "Week" => endDate.AddDays(-7),
                "Month" => endDate.AddMonths(-1),
                "Year" => endDate.AddYears(-1),
                _ => endDate.AddDays(-7)
            };

            var grades = await _context.Grades
                .Where(g => g.StudentId == studentId && g.RecordedAt >= startDate && g.RecordedAt <= endDate)
                .ToListAsync();

            if (period == "Week")
            {
                var daysOfWeek = new[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
                var result = new List<GpaDataPoint>();

                for (int i = 0; i < 7; i++)
                {
                    var dayStart = startDate.AddDays(i);
                    var dayEnd = dayStart.AddDays(1);
                    var dayGrades = grades.Where(g => g.RecordedAt >= dayStart && g.RecordedAt < dayEnd);
                    var averageGpa = dayGrades.Any() ? (double)dayGrades.Average(g => g.Percentage) : 0;

                    result.Add(new GpaDataPoint
                    {
                        Day = daysOfWeek[i],
                        Value = Math.Round(averageGpa, 1)
                    });
                }

                return result;
            }

            // For month and year, group by month
            var monthlyData = grades
                .GroupBy(g => g.RecordedAt.Month)
                .Select(g => new GpaDataPoint
                {
                    Day = g.Key.ToString(),
                    Value = Math.Round((double)g.Average(x => x.Percentage), 1)
                })
                .ToList();

            return monthlyData;
        }

        public async Task<List<AttendanceDataPoint>> GetAttendanceDataAsync(int studentId, string period = "Month")
        {
            var endDate = DateTime.UtcNow;
            var startDate = period switch
            {
                "Week" => endDate.AddDays(-7),
                "Month" => endDate.AddMonths(-1),
                "Year" => endDate.AddYears(-1),
                _ => endDate.AddMonths(-1)
            };

            var attendances = await _context.Attendances
                .Where(a => a.StudentId == studentId && a.Date >= startDate && a.Date <= endDate)
                .ToListAsync();

            var monthlyData = attendances
                .GroupBy(a => a.Date.Month)
                .Select(g => new AttendanceDataPoint
                {
                    Month = g.Key switch
                    {
                        1 => "Jan", 2 => "Feb", 3 => "Mar", 4 => "Apr",
                        5 => "May", 6 => "Jun", 7 => "Jul", 8 => "Aug",
                        9 => "Sep", 10 => "Oct", 11 => "Nov", 12 => "Dec",
                        _ => g.Key.ToString()
                    },
                    Percentage = g.Count(a => a.Status == "Present") * 100.0 / g.Count()
                })
                .ToList();

            return monthlyData;
        }

        public async Task<HomeworkData> GetHomeworkDataAsync(int studentId)
        {
            var assignments = await _context.Assignments
                .Where(a => a.Course.Enrollments.Any(e => e.StudentId == studentId))
                .ToListAsync();

            var submissions = await _context.Submissions
                .Where(s => s.StudentId == studentId)
                .ToListAsync();

            var total = assignments.Count;
            var checkedCount = submissions.Count(s => s.Status == "Graded");
            var pendingCount = submissions.Count(s => s.Status == "Submitted");
            var currentCount = assignments.Count(a => a.DueDate > DateTime.UtcNow && !submissions.Any(s => s.AssignmentId == a.Id));
            var overdueCount = assignments.Count(a => a.DueDate < DateTime.UtcNow && !submissions.Any(s => s.AssignmentId == a.Id));

            return new HomeworkData
            {
                Total = total,
                Statuses = new List<HomeworkStatus>
                {
                    new() { Status = "Checked", Count = checkedCount, Color = "green" },
                    new() { Status = "Pending", Count = pendingCount, Color = "orange" },
                    new() { Status = "Current", Count = currentCount, Color = "blue" },
                    new() { Status = "Overdue", Count = overdueCount, Color = "red" }
                }
            };
        }

        public async Task<TestData> GetTestDataAsync(int studentId)
        {
            var tests = await _context.Tests
                .Where(t => t.Course.Enrollments.Any(e => e.StudentId == studentId))
                .ToListAsync();

            var testResults = await _context.TestResults
                .Where(tr => tr.StudentId == studentId)
                .ToListAsync();

            var total = tests.Count;
            var successfullyCount = testResults.Count(tr => tr.Status == "Completed" && tr.Percentage >= 60);
            var pendingCount = tests.Count(t => t.TestDate > DateTime.UtcNow);
            var failedCount = testResults.Count(tr => tr.Status == "Completed" && tr.Percentage < 60);

            return new TestData
            {
                Total = total,
                Statuses = new List<TestStatus>
                {
                    new() { Status = "Successfully", Count = successfullyCount, Color = "green" },
                    new() { Status = "Pending", Count = pendingCount, Color = "orange" },
                    new() { Status = "Failed", Count = failedCount, Color = "red" }
                }
            };
        }

        public async Task<CalendarData> GetCalendarDataAsync(int studentId, DateTime? date = null)
        {
            var targetDate = date ?? DateTime.UtcNow;
            var startOfWeek = targetDate.AddDays(-(int)targetDate.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(6);

            var schedules = await _context.Schedules
                .Where(s => s.Course.Enrollments.Any(e => e.StudentId == studentId))
                .Where(s => s.StartTime >= startOfWeek && s.StartTime <= endOfWeek)
                .OrderBy(s => s.StartTime)
                .ToListAsync();

            var days = new List<CalendarDay>();
            for (int i = 0; i < 7; i++)
            {
                var day = startOfWeek.AddDays(i);
                days.Add(new CalendarDay
                {
                    DayName = day.ToString("ddd"),
                    DayNumber = day.Day,
                    IsToday = day.Date == DateTime.UtcNow.Date
                });
            }

            var todaySchedule = schedules
                .Where(s => s.StartTime.Date == targetDate.Date)
                .Select(s => new ScheduleItem
                {
                    Time = $"{s.StartTime:HH:mm} - {s.EndTime:HH:mm}",
                    Type = s.Type,
                    Title = s.Title,
                    Color = s.Type == "Workshop" ? "blue" : "light-green"
                })
                .ToList();

            return new CalendarData
            {
                SelectedMonth = targetDate.ToString("MMMM"),
                Days = days,
                TodaySchedule = todaySchedule
            };
        }

        public async Task<RatingData> GetRatingDataAsync(int studentId)
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null) return new RatingData();

            // Calculate group ranking
            var groupStudents = await _context.Students
                .Where(s => s.Group == student.Group)
                .OrderByDescending(s => s.GPA)
                .ToListAsync();

            var groupRank = groupStudents.FindIndex(s => s.Id == studentId) + 1;

            // Calculate flow ranking (all students)
            var allStudents = await _context.Students
                .OrderByDescending(s => s.GPA)
                .ToListAsync();

            var flowRank = allStudents.FindIndex(s => s.Id == studentId) + 1;

            return new RatingData
            {
                GroupRank = groupRank,
                FlowRank = flowRank
            };
        }

        public async Task<CourseInfo> GetCourseInfoAsync(int studentId)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Where(e => e.StudentId == studentId && e.Status == "Active")
                .FirstOrDefaultAsync();

            if (enrollment == null)
            {
                return new CourseInfo
                {
                    CourseName = "No Active Course",
                    Format = "N/A",
                    Duration = "N/A",
                    Access = "Closed",
                    Level = "N/A",
                    NextPayment = "N/A"
                };
            }

            return new CourseInfo
            {
                CourseName = enrollment.Course.Name,
                Format = enrollment.Course.Format,
                Duration = $"{enrollment.Course.Duration} month",
                Access = enrollment.Course.Access,
                Level = enrollment.Course.Level,
                NextPayment = enrollment.Course.EndDate.ToString("dd.MM.yyyy")
            };
        }

        public async Task<List<RewardItem>> GetRewardsDataAsync(int studentId, string period = "Today")
        {
            var endDate = DateTime.UtcNow;
            var startDate = period switch
            {
                "Today" => endDate.Date,
                "Yesterday" => endDate.AddDays(-1).Date,
                "All" => DateTime.MinValue,
                _ => endDate.Date
            };

            var rewards = await _context.Rewards
                .Where(r => r.StudentId == studentId && r.EarnedAt >= startDate && r.EarnedAt <= endDate)
                .OrderByDescending(r => r.EarnedAt)
                .Take(10)
                .ToListAsync();

            return rewards.Select(r => new RewardItem
            {
                Type = r.Type,
                Date = r.EarnedAt.ToString("dd.MM.yyyy"),
                Points = r.Points,
                Icon = r.Icon ?? GetIconForType(r.Type)
            }).ToList();
        }

        public async Task<List<LeaderboardEntry>> GetLeaderboardDataAsync(int studentId, string filter = "Group")
        {
            var student = await _context.Students.FindAsync(studentId);
            if (student == null) return new List<LeaderboardEntry>();

            var students = filter == "Group" && !string.IsNullOrEmpty(student.Group)
                ? await _context.Students
                    .Include(s => s.User)
                    .Where(s => s.Group == student.Group)
                    .OrderByDescending(s => s.GPA)
                    .Take(10)
                    .ToListAsync()
                : await _context.Students
                    .Include(s => s.User)
                    .OrderByDescending(s => s.GPA)
                    .Take(10)
                    .ToListAsync();

            return students.Select((s, index) => new LeaderboardEntry
            {
                Rank = index + 1,
                Name = $"{s.User.FirstName} {s.User.LastName}",
                Score = (int)(s.GPA * 1000),
                Avatar = s.User.Avatar ?? "/images/avatar.png"
            }).ToList();
        }

        private async Task<int> GetTotalRewardsAsync(int studentId)
        {
            return await _context.Rewards
                .Where(r => r.StudentId == studentId)
                .SumAsync(r => r.Points);
        }

        private async Task<int> GetUnreadNotificationsCountAsync(int userId)
        {
            return await _context.Notifications
                .CountAsync(n => n.UserId == userId && !n.IsRead);
        }

        private static string GetIconForType(string type)
        {
            return type.ToLower() switch
            {
                "marks" => "checkmark",
                "homework" => "diamond",
                "test" => "trophy",
                "attendance" => "star",
                _ => "star"
            };
        }
    }
}
