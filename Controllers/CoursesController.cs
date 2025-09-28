using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using StudentDashboard.Data;
using StudentDashboard.Models;

namespace StudentDashboard.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ApplicationDbContext context, ILogger<CoursesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst("nameid")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return RedirectToAction("Login", "Auth");
                }

                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.UserId == userId);

                if (student == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                var courses = await _context.Enrollments
                    .Include(e => e.Course)
                    .Where(e => e.StudentId == student.Id)
                    .Select(e => e.Course)
                    .ToListAsync();

                return View(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading courses page");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst("nameid")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized();
                }

                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.UserId == userId);

                if (student == null)
                {
                    return NotFound();
                }

                var course = await _context.Courses
                    .Include(c => c.Assignments)
                    .Include(c => c.Tests)
                    .Include(c => c.Schedules)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (course == null)
                {
                    return NotFound();
                }

                // Check if student is enrolled
                var enrollment = await _context.Enrollments
                    .FirstOrDefaultAsync(e => e.StudentId == student.Id && e.CourseId == id);

                if (enrollment == null)
                {
                    return Forbid();
                }

                return View(course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading course details");
                return StatusCode(500);
            }
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> Enroll([FromBody] EnrollRequest request)
        {
            try
            {
                var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst("nameid")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized();
                }

                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.UserId == userId);

                if (student == null)
                {
                    return NotFound();
                }

                var course = await _context.Courses
                    .FirstOrDefaultAsync(c => c.Code == request.CourseCode);

                if (course == null)
                {
                    return NotFound(new { Message = "Course not found" });
                }

                // Check if already enrolled
                var existingEnrollment = await _context.Enrollments
                    .FirstOrDefaultAsync(e => e.StudentId == student.Id && e.CourseId == course.Id);

                if (existingEnrollment != null)
                {
                    return BadRequest(new { Message = "Already enrolled in this course" });
                }

                // Create enrollment
                var enrollment = new Enrollment
                {
                    StudentId = student.Id,
                    CourseId = course.Id,
                    EnrollmentDate = DateTime.UtcNow,
                    Status = "Active"
                };

                _context.Enrollments.Add(enrollment);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Successfully enrolled in course" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error enrolling in course");
                return StatusCode(500, new { Message = "An error occurred while enrolling" });
            }
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> CompleteCourse(int id)
        {
            try
            {
                var userIdClaim = User.FindFirst("sub")?.Value ?? User.FindFirst("nameid")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                {
                    return Unauthorized();
                }

                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.UserId == userId);

                if (student == null)
                {
                    return NotFound();
                }

                var enrollment = await _context.Enrollments
                    .FirstOrDefaultAsync(e => e.StudentId == student.Id && e.CourseId == id);

                if (enrollment == null)
                {
                    return NotFound();
                }

                enrollment.Status = "Completed";
                enrollment.CompletionDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(new { Message = "Course marked as completed" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error completing course");
                return StatusCode(500, new { Message = "An error occurred while completing course" });
            }
        }
    }

    public class EnrollRequest
    {
        public string CourseCode { get; set; } = string.Empty;
    }
}
