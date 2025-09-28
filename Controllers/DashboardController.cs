using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StudentDashboard.Services;
using StudentDashboard.Models;

namespace StudentDashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IDashboardService dashboardService, ILogger<DashboardController> logger)
        {
            _dashboardService = dashboardService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            try
            {
                var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst("nameid")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new { Message = "Invalid user token" });
                }

                var dashboardData = await _dashboardService.GetDashboardDataAsync(userId);
                return Ok(dashboardData);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning("Dashboard data not found: {Error}", ex.Message);
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting dashboard data");
                return StatusCode(500, new { Message = "An error occurred while getting dashboard data" });
            }
        }

        [HttpGet("gpa")]
        public async Task<IActionResult> GetGpaData([FromQuery] string period = "Week")
        {
            try
            {
                var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst("nameid")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new { Message = "Invalid user token" });
                }

                var gpaData = await _dashboardService.GetGpaDataAsync(userId, period);
                return Ok(gpaData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting GPA data");
                return StatusCode(500, new { Message = "An error occurred while getting GPA data" });
            }
        }

        [HttpGet("attendance")]
        public async Task<IActionResult> GetAttendanceData([FromQuery] string period = "Month")
        {
            try
            {
                var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst("nameid")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new { Message = "Invalid user token" });
                }

                var attendanceData = await _dashboardService.GetAttendanceDataAsync(userId, period);
                return Ok(attendanceData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting attendance data");
                return StatusCode(500, new { Message = "An error occurred while getting attendance data" });
            }
        }

        [HttpGet("homework")]
        public async Task<IActionResult> GetHomeworkData()
        {
            try
            {
                var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst("nameid")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new { Message = "Invalid user token" });
                }

                var homeworkData = await _dashboardService.GetHomeworkDataAsync(userId);
                return Ok(homeworkData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting homework data");
                return StatusCode(500, new { Message = "An error occurred while getting homework data" });
            }
        }

        [HttpGet("tests")]
        public async Task<IActionResult> GetTestData()
        {
            try
            {
                var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst("nameid")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new { Message = "Invalid user token" });
                }

                var testData = await _dashboardService.GetTestDataAsync(userId);
                return Ok(testData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting test data");
                return StatusCode(500, new { Message = "An error occurred while getting test data" });
            }
        }

        [HttpGet("calendar")]
        public async Task<IActionResult> GetCalendarData([FromQuery] DateTime? date = null)
        {
            try
            {
                var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst("nameid")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new { Message = "Invalid user token" });
                }

                var calendarData = await _dashboardService.GetCalendarDataAsync(userId, date);
                return Ok(calendarData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting calendar data");
                return StatusCode(500, new { Message = "An error occurred while getting calendar data" });
            }
        }

        [HttpGet("rating")]
        public async Task<IActionResult> GetRatingData()
        {
            try
            {
                var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst("nameid")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new { Message = "Invalid user token" });
                }

                var ratingData = await _dashboardService.GetRatingDataAsync(userId);
                return Ok(ratingData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting rating data");
                return StatusCode(500, new { Message = "An error occurred while getting rating data" });
            }
        }

        [HttpGet("course")]
        public async Task<IActionResult> GetCourseInfo()
        {
            try
            {
                var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst("nameid")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new { Message = "Invalid user token" });
                }

                var courseInfo = await _dashboardService.GetCourseInfoAsync(userId);
                return Ok(courseInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting course info");
                return StatusCode(500, new { Message = "An error occurred while getting course info" });
            }
        }

        [HttpGet("rewards")]
        public async Task<IActionResult> GetRewardsData([FromQuery] string period = "Today")
        {
            try
            {
                var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst("nameid")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new { Message = "Invalid user token" });
                }

                var rewardsData = await _dashboardService.GetRewardsDataAsync(userId, period);
                return Ok(rewardsData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting rewards data");
                return StatusCode(500, new { Message = "An error occurred while getting rewards data" });
            }
        }

        [HttpGet("leaderboard")]
        public async Task<IActionResult> GetLeaderboardData([FromQuery] string filter = "Group")
        {
            try
            {
                var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst("nameid")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized(new { Message = "Invalid user token" });
                }

                var leaderboardData = await _dashboardService.GetLeaderboardDataAsync(userId, filter);
                return Ok(leaderboardData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting leaderboard data");
                return StatusCode(500, new { Message = "An error occurred while getting leaderboard data" });
            }
        }
    }
}
