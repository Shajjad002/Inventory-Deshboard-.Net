namespace StudentDashboard.Models
{
    public class DashboardViewModel
    {
        public UserProfile UserProfile { get; set; } = new();
        public ProgressGpaData ProgressGpa { get; set; } = new();
        public AttendanceData Attendance { get; set; } = new();
        public HomeworkData Homework { get; set; } = new();
        public TestData Test { get; set; } = new();
        public CalendarData Calendar { get; set; } = new();
        public RatingData Rating { get; set; } = new();
        public CourseInfo CourseInfo { get; set; } = new();
        public RewardsData Rewards { get; set; } = new();
        public LeaderboardData Leaderboard { get; set; } = new();
    }

    public class UserProfile
    {
        public string Name { get; set; } = "Phillip Vetrov";
        public string Group { get; set; } = "DD 1234";
        public string Avatar { get; set; } = "/images/avatar.png";
        public int Stars { get; set; } = 5456;
        public int Trophies { get; set; } = 5456;
        public int Diamonds { get; set; } = 5456;
        public int Notifications { get; set; } = 5456;
    }

    public class ProgressGpaData
    {
        public string Title { get; set; } = "Progress GPA";
        public string Subtitle { get; set; } = "8 GPA / Week";
        public string SelectedPeriod { get; set; } = "Week";
        public List<GpaDataPoint> DataPoints { get; set; } = new();
    }

    public class GpaDataPoint
    {
        public string Day { get; set; } = "";
        public double Value { get; set; }
    }

    public class AttendanceData
    {
        public string Title { get; set; } = "Attendance";
        public string SelectedPeriod { get; set; } = "Month";
        public List<AttendanceDataPoint> DataPoints { get; set; } = new();
    }

    public class AttendanceDataPoint
    {
        public string Month { get; set; } = "";
        public double Percentage { get; set; }
    }

    public class HomeworkData
    {
        public string Title { get; set; } = "Homework";
        public int Total { get; set; } = 291;
        public List<HomeworkStatus> Statuses { get; set; } = new();
    }

    public class HomeworkStatus
    {
        public string Status { get; set; } = "";
        public int Count { get; set; }
        public string Color { get; set; } = "";
    }

    public class TestData
    {
        public string Title { get; set; } = "Test";
        public int Total { get; set; } = 45;
        public List<TestStatus> Statuses { get; set; } = new();
    }

    public class TestStatus
    {
        public string Status { get; set; } = "";
        public int Count { get; set; }
        public string Color { get; set; } = "";
    }

    public class CalendarData
    {
        public string Title { get; set; } = "Calendar";
        public string SelectedMonth { get; set; } = "January";
        public List<CalendarDay> Days { get; set; } = new();
        public List<ScheduleItem> TodaySchedule { get; set; } = new();
    }

    public class CalendarDay
    {
        public string DayName { get; set; } = "";
        public int DayNumber { get; set; }
        public bool IsToday { get; set; }
    }

    public class ScheduleItem
    {
        public string Time { get; set; } = "";
        public string Type { get; set; } = "";
        public string Title { get; set; } = "";
        public string Color { get; set; } = "";
    }

    public class RatingData
    {
        public string Title { get; set; } = "Rating";
        public int GroupRank { get; set; } = 5;
        public int FlowRank { get; set; } = 15;
    }

    public class CourseInfo
    {
        public string Title { get; set; } = "About Course";
        public string CourseName { get; set; } = "UX/UI Design";
        public string Format { get; set; } = "Online";
        public string Duration { get; set; } = "6 month";
        public string Access { get; set; } = "Open";
        public string Level { get; set; } = "Basic";
        public string NextPayment { get; set; } = "6.10.2021";
    }

    public class RewardsData
    {
        public string Title { get; set; } = "Rewards";
        public string SelectedPeriod { get; set; } = "Today";
        public List<RewardItem> Rewards { get; set; } = new();
    }

    public class RewardItem
    {
        public string Type { get; set; } = "";
        public string Date { get; set; } = "";
        public int Points { get; set; }
        public string Icon { get; set; } = "";
    }

    public class LeaderboardData
    {
        public string Title { get; set; } = "Leaderboard";
        public string SelectedFilter { get; set; } = "Group";
        public List<LeaderboardEntry> Entries { get; set; } = new();
    }

    public class LeaderboardEntry
    {
        public int Rank { get; set; }
        public string Name { get; set; } = "";
        public int Score { get; set; }
        public string Avatar { get; set; } = "";
    }
}
