using StudentDashboard.Models;

namespace StudentDashboard.Services
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardDataAsync(int studentId);
        Task<ProgressGpaData> GetProgressGpaDataAsync(int studentId, string period);
        Task<AttendanceData> GetAttendanceDataAsync(int studentId, string period);
        Task<HomeworkData> GetHomeworkDataAsync(int studentId);
        Task<TestData> GetTestDataAsync(int studentId);
        Task<CalendarData> GetCalendarDataAsync(int studentId, DateTime month);
        Task<RatingData> GetRatingDataAsync(int studentId);
        Task<CourseInfo> GetCourseInfoAsync(int studentId);
        Task<RewardsData> GetRewardsDataAsync(int studentId, string period);
        Task<LeaderboardData> GetLeaderboardDataAsync(int studentId, string filter);
        Task<List<Notification>> GetNotificationsAsync(int studentId, int count = 10);
        Task MarkNotificationAsReadAsync(int notificationId);
        Task<Student?> GetStudentByUserIdAsync(int userId);
    }
}
