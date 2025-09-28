using Microsoft.AspNetCore.Mvc;
using StudentDashboard.Models;

namespace StudentDashboard.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = CreateSampleData();
            return View(viewModel);
        }

        private DashboardViewModel CreateSampleData()
        {
            return new DashboardViewModel
            {
                ProgressGpa = new ProgressGpaData
                {
                    DataPoints = new List<GpaDataPoint>
                    {
                        new() { Day = "Sun", Value = 0 },
                        new() { Day = "Mon", Value = 0 },
                        new() { Day = "Tue", Value = 6 },
                        new() { Day = "Wed", Value = 0 },
                        new() { Day = "Thu", Value = 9 },
                        new() { Day = "Fri", Value = 11 },
                        new() { Day = "Sat", Value = 0 }
                    }
                },
                Attendance = new AttendanceData
                {
                    DataPoints = new List<AttendanceDataPoint>
                    {
                        new() { Month = "Aug", Percentage = 75 },
                        new() { Month = "Sep", Percentage = 80 },
                        new() { Month = "Oct", Percentage = 85 },
                        new() { Month = "Nov", Percentage = 78 },
                        new() { Month = "Dec", Percentage = 82 },
                        new() { Month = "Jan", Percentage = 82 },
                        new() { Month = "Feb", Percentage = 85 }
                    }
                },
                Homework = new HomeworkData
                {
                    Statuses = new List<HomeworkStatus>
                    {
                        new() { Status = "Checked", Count = 263, Color = "green" },
                        new() { Status = "Pending", Count = 0, Color = "orange" },
                        new() { Status = "Current", Count = 1, Color = "blue" },
                        new() { Status = "Overdue", Count = 27, Color = "red" }
                    }
                },
                Test = new TestData
                {
                    Statuses = new List<TestStatus>
                    {
                        new() { Status = "Successfully", Count = 42, Color = "green" },
                        new() { Status = "Pending", Count = 2, Color = "orange" },
                        new() { Status = "Failed", Count = 1, Color = "red" }
                    }
                },
                Calendar = new CalendarData
                {
                    Days = new List<CalendarDay>
                    {
                        new() { DayName = "Sun", DayNumber = 2, IsToday = false },
                        new() { DayName = "Mon", DayNumber = 3, IsToday = false },
                        new() { DayName = "Tue", DayNumber = 4, IsToday = false },
                        new() { DayName = "Wed", DayNumber = 5, IsToday = true },
                        new() { DayName = "Thu", DayNumber = 6, IsToday = false },
                        new() { DayName = "Fri", DayNumber = 7, IsToday = false },
                        new() { DayName = "Sat", DayNumber = 8, IsToday = false }
                    },
                    TodaySchedule = new List<ScheduleItem>
                    {
                        new() { Time = "08:30 - 09:20", Type = "Workshop", Title = "Lesson Dashboard", Color = "blue" },
                        new() { Time = "10:00 - 11:00", Type = "Lecture", Title = "Lesson UX Interface", Color = "light-green" },
                        new() { Time = "11:30 - 12:20", Type = "Workshop", Title = "Lesson Planning", Color = "blue" },
                        new() { Time = "13:00 - 14:00", Type = "Lecture", Title = "Lesson Composition", Color = "light-green" },
                        new() { Time = "14:30 - 15:30", Type = "Lecture", Title = "Lesson Prototype", Color = "light-green" }
                    }
                },
                Rewards = new RewardsData
                {
                    Rewards = new List<RewardItem>
                    {
                        new() { Type = "Marks", Date = "27.08.2021", Points = 5, Icon = "checkmark" },
                        new() { Type = "Homework", Date = "27.08.2021", Points = 1, Icon = "diamond" },
                        new() { Type = "Marks", Date = "27.08.2021", Points = 2, Icon = "checkmark" },
                        new() { Type = "Test", Date = "27.08.2021", Points = 10, Icon = "trophy" }
                    }
                },
                Leaderboard = new LeaderboardData
                {
                    Entries = new List<LeaderboardEntry>
                    {
                        new() { Rank = 4, Name = "Vetrov Andrew Alexandrovich", Score = 7643, Avatar = "/images/avatar1.png" },
                        new() { Rank = 3, Name = "David Patrick Boreanaz", Score = 4767, Avatar = "/images/avatar2.png" },
                        new() { Rank = 2, Name = "William Bradley Pitt", Score = 4667, Avatar = "/images/avatar3.png" },
                        new() { Rank = 1, Name = "Armand Douglas Hammer", Score = 4555, Avatar = "/images/avatar4.png" }
                    }
                }
            };
        }
    }
}
