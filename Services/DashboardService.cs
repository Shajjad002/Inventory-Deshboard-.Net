using Microsoft.EntityFrameworkCore;
using StudentDashboard.Data;
using StudentDashboard.Models;

namespace StudentDashboard.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DashboardService> _logger;

        public DashboardService(ApplicationDbContext context, ILogger<DashboardService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<DashboardViewModel> GetDashboardDataAsync(int studentId)
        {
            try
            {
                var student = await _context.Students
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.Id == studentId);

                if (student == null)
                    throw new ArgumentException("Student not found");

                return new DashboardViewModel
                {
                    UserProfile = new UserProfile
                    {
                        Name = $"{student.User.FirstName} {student.User.LastName}",
                        Group = student.Group ?? "N/A",
                        Avatar = student.User.Avatar ?? "/images/avatar.png",
                        Stars = await GetTotalRewardPointsAsync(studentId, "Stars"),
                        Trophies = await GetTotalRewardPointsAsync(studentId, "Trophies"),
                        Diamonds = await GetTotalRewardPointsAsync(studentId, "Diamonds"),
                        Notifications = await GetUnreadNotificationCountAsync(studentId)
                    },
                    ProgressGpa = await GetProgressGpaDataAsync(studentId, "Week"),
                    Attendance = await GetAttendanceDataAsync(studentId, "Month"),
                    Homework = await GetHomeworkDataAsync(studentId),
                    Test = await GetTestDataAsync(studentId),
                    Calendar = await GetCalendarDataAsync(studentId, DateTime.Now),
                    Rating = await GetRatingDataAsync(studentId),
                    CourseInfo = await GetCourseInfoAsync(studentId),
                    Rewards = await GetRewardsDataAsync(studentId, "Today"),
                    Leaderboard = await GetLeaderboardDataAsync(studentId, "Group")
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting dashboard data for student {StudentId}", studentId);
                throw;
            }
        }

        public async Task<ProgressGpaData> GetProgressGpaDataAsync(int studentId, string period)
        {
            var endDate = DateTime.Now;
            var startDate = period switch
            {
                "Week" => endDate.AddDays(-7),
                "Month" => endDate.AddMonths(-1),
                "Year" => endDate.AddYears(-1),
                _ => endDate.AddDays(-7)
            };

            var grades = await _context.Grades
                .Where(g => g.StudentId == studentId && g.CreatedAt >= startDate && g.CreatedAt <= endDate)
                .ToListAsync();

            var dataPoints = new List<GpaDataPoint>();
            var days = period == "Week" ? 
                Enumerable.Range(0, 7).Select(i => DateTime.Now.AddDays(-i).DayOfWeek.ToString().Substring(0, 3)) :
                new[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

            foreach (var day in days)
            {
                var dayGrades = grades.Where(g => g.CreatedAt.DayOfWeek.ToString().Substring(0, 3) == day);
                var averageGpa = dayGrades.Any() ? (double)(dayGrades.Average(g => g.Percentage) / 10) : 0;
                dataPoints.Add(new GpaDataPoint { Day = day, Value = Math.Round(averageGpa, 1) });
            }

            return new ProgressGpaData
            {
                SelectedPeriod = period,
                DataPoints = dataPoints.OrderBy(d => Array.IndexOf(days.ToArray(), d.Day)).ToList()
            };
        }

        public async Task<AttendanceData> GetAttendanceDataAsync(int studentId, string period)
        {
            var endDate = DateTime.Now;
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

            var dataPoints = new List<AttendanceDataPoint>();
            var months = new[] { "Aug", "Sep", "Oct", "Nov", "Dec", "Jan", "Feb" };

            foreach (var month in months)
            {
                var monthAttendances = attendances.Where(a => a.Date.ToString("MMM") == month);
                var percentage = monthAttendances.Any() ? 
                    (monthAttendances.Count(a => a.Status == "Present") * 100.0 / monthAttendances.Count()) : 0;
                dataPoints.Add(new AttendanceDataPoint { Month = month, Percentage = Math.Round(percentage, 1) });
            }

            return new AttendanceData
            {
                SelectedPeriod = period,
                DataPoints = dataPoints
            };
        }

        public async Task<HomeworkData> GetHomeworkDataAsync(int studentId)
        {
            var assignments = await _context.Assignments
                .Include(a => a.Grades.Where(g => g.StudentId == studentId))
                .Where(a => a.Status == "Active")
                .ToListAsync();

            var total = assignments.Count;
            var checkedCount = assignments.Count(a => a.Grades.Any());
            var pendingCount = assignments.Count(a => !a.Grades.Any() && a.DueDate > DateTime.Now);
            var currentCount = assignments.Count(a => !a.Grades.Any() && a.DueDate <= DateTime.Now && a.DueDate >= DateTime.Now.AddDays(-7));
            var overdueCount = assignments.Count(a => !a.Grades.Any() && a.DueDate < DateTime.Now);

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
                .Include(t => t.Grades.Where(g => g.StudentId == studentId))
                .Where(t => t.Status != "Cancelled")
                .ToListAsync();

            var total = tests.Count;
            var successfulCount = tests.Count(t => t.Grades.Any() && t.Grades.First().Percentage >= 60);
            var pendingCount = tests.Count(t => !t.Grades.Any() && t.TestDate > DateTime.Now);
            var failedCount = tests.Count(t => t.Grades.Any() && t.Grades.First().Percentage < 60);

            return new TestData
            {
                Total = total,
                Statuses = new List<TestStatus>
                {
                    new() { Status = "Successfully", Count = successfulCount, Color = "green" },
                    new() { Status = "Pending", Count = pendingCount, Color = "orange" },
                    new() { Status = "Failed", Count = failedCount, Color = "red" }
                }
            };
        }

        public async Task<CalendarData> GetCalendarDataAsync(int studentId, DateTime month)
        {
            var startOfMonth = new DateTime(month.Year, month.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var schedules = await _context.Schedules
                .Include(s => s.Course)
                .Where(s => s.StartTime >= startOfMonth && s.StartTime <= endOfMonth)
                .OrderBy(s => s.StartTime)
                .ToListAsync();

            var today = DateTime.Now.Date;
            var days = new List<CalendarDay>();
            var weekDays = new[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

            for (int i = 0; i < 7; i++)
            {
                var date = today.AddDays(i - (int)today.DayOfWeek);
                days.Add(new CalendarDay
                {
                    DayName = weekDays[(int)date.DayOfWeek],
                    DayNumber = date.Day,
                    IsToday = date == today
                });
            }

            var todaySchedule = schedules
                .Where(s => s.StartTime.Date == today)
                .Select(s => new ScheduleItem
                {
                    Time = $"{s.StartTime:HH:mm} - {s.EndTime:HH:mm}",
                    Type = s.Type,
                    Title = s.Title,
                    Color = s.Type.ToLower() switch
                    {
                        "workshop" => "blue",
                        "lecture" => "light-green",
                        _ => "blue"
                    }
                })
                .ToList();

            return new CalendarData
            {
                SelectedMonth = month.ToString("MMMM"),
                Days = days,
                TodaySchedule = todaySchedule
            };
        }

        public async Task<RatingData> GetRatingDataAsync(int studentId)
        {
            var student = await _context.Students
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null) return new RatingData();

            var groupStudents = await _context.Students
                .Where(s => s.Group == student.Group)
                .ToListAsync();

            var groupRank = groupStudents
                .OrderByDescending(s => GetStudentScore(s.Id))
                .ToList()
                .FindIndex(s => s.Id == studentId) + 1;

            var allStudents = await _context.Students.ToListAsync();
            var flowRank = allStudents
                .OrderByDescending(s => GetStudentScore(s.Id))
                .ToList()
                .FindIndex(s => s.Id == studentId) + 1;

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

            if (enrollment == null) return new CourseInfo();

            return new CourseInfo
            {
                CourseName = enrollment.Course.Name,
                Format = enrollment.Course.Format,
                Duration = $"{enrollment.Course.Duration} month",
                Access = enrollment.Course.Access,
                Level = enrollment.Course.Level,
                NextPayment = enrollment.EnrolledAt.AddMonths(2).ToString("dd.MM.yyyy")
            };
        }

        public async Task<RewardsData> GetRewardsDataAsync(int studentId, string period)
        {
            var endDate = DateTime.Now;
            var startDate = period switch
            {
                "Today" => endDate.Date,
                "Yesterday" => endDate.AddDays(-1).Date,
                "All" => DateTime.MinValue,
                _ => endDate.Date
            };

            var rewards = await _context.Rewards
                .Where(r => r.StudentId == studentId && r.AwardedAt >= startDate && r.AwardedAt <= endDate)
                .OrderByDescending(r => r.AwardedAt)
                .Take(10)
                .ToListAsync();

            var rewardItems = rewards.Select(r => new RewardItem
            {
                Type = r.Type,
                Date = r.AwardedAt.ToString("dd.MM.yyyy"),
                Points = r.Points,
                Icon = r.Type.ToLower() switch
                {
                    "marks" => "checkmark",
                    "homework" => "diamond",
                    "test" => "trophy",
                    _ => "star"
                }
            }).ToList();

            return new RewardsData
            {
                SelectedPeriod = period,
                Rewards = rewardItems
            };
        }

        public async Task<LeaderboardData> GetLeaderboardDataAsync(int studentId, string filter)
        {
            var students = filter == "Group" 
                ? await _context.Students
                    .Include(s => s.User)
                    .Where(s => s.Group == _context.Students.First(s => s.Id == studentId).Group)
                    .ToListAsync()
                : await _context.Students
                    .Include(s => s.User)
                    .ToListAsync();

            var leaderboardEntries = students
                .Select(s => new LeaderboardEntry
                {
                    Rank = 0, // Will be set after ordering
                    Name = $"{s.User.FirstName} {s.User.LastName}",
                    Score = GetStudentScore(s.Id),
                    Avatar = s.User.Avatar ?? "/images/avatar.png"
                })
                .OrderByDescending(e => e.Score)
                .Take(10)
                .ToList();

            for (int i = 0; i < leaderboardEntries.Count; i++)
            {
                leaderboardEntries[i].Rank = i + 1;
            }

            return new LeaderboardData
            {
                SelectedFilter = filter,
                Entries = leaderboardEntries
            };
        }

        public async Task<List<Notification>> GetNotificationsAsync(int studentId, int count = 10)
        {
            return await _context.Notifications
                .Where(n => n.StudentId == studentId)
                .OrderByDescending(n => n.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task MarkNotificationAsReadAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                notification.ReadAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Student?> GetStudentByUserIdAsync(int userId)
        {
            return await _context.Students
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }

        private async Task<int> GetTotalRewardPointsAsync(int studentId, string type)
        {
            return await _context.Rewards
                .Where(r => r.StudentId == studentId && r.Type == type)
                .SumAsync(r => r.Points);
        }

        private async Task<int> GetUnreadNotificationCountAsync(int studentId)
        {
            return await _context.Notifications
                .CountAsync(n => n.StudentId == studentId && !n.IsRead);
        }

        private int GetStudentScore(int studentId)
        {
            // This is a simplified scoring system
            // In a real application, this would be more complex
            return new Random().Next(1000, 10000);
        }
    }
}
