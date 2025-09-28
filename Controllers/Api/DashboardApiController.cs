using Microsoft.AspNetCore.Mvc;
using StudentDashboard.Services;

namespace StudentDashboard.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardApiController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly ILogger<DashboardApiController> _logger;

        public DashboardApiController(IDashboardService dashboardService, ILogger<DashboardApiController> logger)
        {
            _dashboardService = dashboardService;
            _logger = logger;
        }

        [HttpGet("data/{studentId}")]
        public async Task<IActionResult> GetDashboardData(int studentId)
        {
            try
            {
                var data = await _dashboardService.GetDashboardDataAsync(studentId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting dashboard data for student {StudentId}", studentId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("progress-gpa/{studentId}")]
        public async Task<IActionResult> GetProgressGpa(int studentId, [FromQuery] string period = "Week")
        {
            try
            {
                var data = await _dashboardService.GetProgressGpaDataAsync(studentId, period);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting progress GPA data for student {StudentId}", studentId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("attendance/{studentId}")]
        public async Task<IActionResult> GetAttendance(int studentId, [FromQuery] string period = "Month")
        {
            try
            {
                var data = await _dashboardService.GetAttendanceDataAsync(studentId, period);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting attendance data for student {StudentId}", studentId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("homework/{studentId}")]
        public async Task<IActionResult> GetHomework(int studentId)
        {
            try
            {
                var data = await _dashboardService.GetHomeworkDataAsync(studentId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting homework data for student {StudentId}", studentId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("tests/{studentId}")]
        public async Task<IActionResult> GetTests(int studentId)
        {
            try
            {
                var data = await _dashboardService.GetTestDataAsync(studentId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting test data for student {StudentId}", studentId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("calendar/{studentId}")]
        public async Task<IActionResult> GetCalendar(int studentId, [FromQuery] DateTime? month = null)
        {
            try
            {
                var targetMonth = month ?? DateTime.Now;
                var data = await _dashboardService.GetCalendarDataAsync(studentId, targetMonth);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting calendar data for student {StudentId}", studentId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("rating/{studentId}")]
        public async Task<IActionResult> GetRating(int studentId)
        {
            try
            {
                var data = await _dashboardService.GetRatingDataAsync(studentId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting rating data for student {StudentId}", studentId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("course-info/{studentId}")]
        public async Task<IActionResult> GetCourseInfo(int studentId)
        {
            try
            {
                var data = await _dashboardService.GetCourseInfoAsync(studentId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting course info for student {StudentId}", studentId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("rewards/{studentId}")]
        public async Task<IActionResult> GetRewards(int studentId, [FromQuery] string period = "Today")
        {
            try
            {
                var data = await _dashboardService.GetRewardsDataAsync(studentId, period);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting rewards data for student {StudentId}", studentId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("leaderboard/{studentId}")]
        public async Task<IActionResult> GetLeaderboard(int studentId, [FromQuery] string filter = "Group")
        {
            try
            {
                var data = await _dashboardService.GetLeaderboardDataAsync(studentId, filter);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting leaderboard data for student {StudentId}", studentId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("notifications/{studentId}")]
        public async Task<IActionResult> GetNotifications(int studentId, [FromQuery] int count = 10)
        {
            try
            {
                var data = await _dashboardService.GetNotificationsAsync(studentId, count);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting notifications for student {StudentId}", studentId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("notifications/{notificationId}/read")]
        public async Task<IActionResult> MarkNotificationAsRead(int notificationId)
        {
            try
            {
                await _dashboardService.MarkNotificationAsReadAsync(notificationId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error marking notification {NotificationId} as read", notificationId);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
