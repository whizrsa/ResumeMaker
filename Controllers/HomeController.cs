using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResumeMaker.Data;
using ResumeMaker.Models;

namespace ResumeMaker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var resumes = _context.Resumes.ToList();
            return View(resumes);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            var resume = new Resume();
            return View(resume);
        }

        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(
            Resume resume,
            string skillsInput,
            List<Experience> experiences,
            List<Education> educationHistory,
            List<Project> projects,
            List<Certification> certifications,
            List<Language> languages)
        {
            // Always assign posted collections back to the model so the view preserves user data on errors
            resume.Experiences = experiences ?? new List<Experience>();
            resume.EducationHistory = educationHistory ?? new List<Education>();
            resume.Projects = projects ?? new List<Project>();
            resume.Certifications = certifications ?? new List<Certification>();
            resume.Languages = languages ?? new List<Language>();

            // Normalize skills from comma-separated input
            if (!string.IsNullOrWhiteSpace(skillsInput))
            {
                resume.Skills = skillsInput
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .ToList();
            }
            else
            {
                resume.Skills = new List<string>();
            }

            // Server-side validation for required sections
            if (resume.Experiences == null || resume.Experiences.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "At least one work experience is required.");
            }
            if (resume.EducationHistory == null || resume.EducationHistory.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "At least one education entry is required.");
            }
            if (resume.Skills == null || resume.Skills.Count < 3)
            {
                ModelState.AddModelError(string.Empty, "Please enter at least 3 skills.");
            }

            if (ModelState.IsValid)
            {
                _context.Resumes.Add(resume);
                _context.SaveChanges();
                return RedirectToAction("View", new { id = resume.Id });
            }

            // When invalid, return the view with the fully populated model
            return View(resume);
        }

        public IActionResult Edit(int? resumeId)
        {
            if (resumeId == null)
            {
                return NotFound();
            }

            var resume = _context.Resumes
                .Include(r => r.Experiences)
                .Include(r => r.EducationHistory)
                .Include(r => r.Certifications)
                .Include(r => r.Projects)
                .Include(r => r.Languages)
                .FirstOrDefault(r => r.Id == resumeId);

            if (resume == null)
            {
                return NotFound();
            }

            return View(resume);
        }

        [HttpPost]
        public IActionResult Edit(Resume resume, string skillsInput)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(skillsInput))
                {
                    resume.Skills = skillsInput.Split(',').Select(s => s.Trim()).ToList();
                }

                _context.Resumes.Update(resume);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resume);
        }

        public IActionResult View(int? id, string? theme)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = _context.Resumes
                .Include(r => r.Experiences)
                .Include(r => r.EducationHistory)
                .Include(r => r.Certifications)
                .Include(r => r.Projects)
                .Include(r => r.Languages)
                .FirstOrDefault(r => r.Id == id);

            if (resume == null)
            {
                return NotFound();
            }

            // Validate and pass theme to view
            var allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "minimal", "classic", "modern" };
            var chosen = !string.IsNullOrWhiteSpace(theme) && allowed.Contains(theme) ? theme!.ToLowerInvariant() : "minimal";
            ViewBag.Theme = chosen;

            return View(resume);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resume = _context.Resumes.Find(id);
            if (resume == null)
            {
                return NotFound();
            }

            _context.Resumes.Remove(resume);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
