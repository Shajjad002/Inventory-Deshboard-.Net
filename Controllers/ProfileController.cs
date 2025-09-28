using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentDashboard.Data;
using StudentDashboard.Models;

namespace StudentDashboard.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ApplicationDbContext context, ILogger<ProfileController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            // For demo purposes, using student ID 1
            var studentId = 1;
            var student = await _context.Students
                .Include(s => s.User)
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var studentId = 1; // For demo purposes
            var student = await _context.Students
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var student = await _context.Students
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.Id == model.Id);

                if (student == null)
                {
                    return NotFound();
                }

                // Update user information
                student.User.FirstName = model.User.FirstName;
                student.User.LastName = model.User.LastName;
                student.User.Email = model.User.Email;
                student.User.UpdatedAt = DateTime.UtcNow;

                // Update student information
                student.Group = model.Group;
                student.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Profile updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating profile for student {StudentId}", model.Id);
                ModelState.AddModelError("", "An error occurred while updating the profile.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            var studentId = 1; // For demo purposes
            var student = await _context.Students
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSettings([FromBody] SettingsViewModel settings)
        {
            try
            {
                var studentId = 1; // For demo purposes
                var student = await _context.Students
                    .Include(s => s.User)
                    .FirstOrDefaultAsync(s => s.Id == studentId);

                if (student == null)
                {
                    return NotFound();
                }

                // Update settings
                student.User.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Settings updated successfully!" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating settings for student");
                return Json(new { success = false, message = "An error occurred while updating settings." });
            }
        }
    }

    public class SettingsViewModel
    {
        public string Theme { get; set; } = "light";
        public string Language { get; set; } = "en";
        public bool EmailNotifications { get; set; } = true;
        public bool PushNotifications { get; set; } = true;
        public bool DarkMode { get; set; } = false;
    }
}
