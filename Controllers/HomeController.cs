using System.Diagnostics;
using System.Security.Claims;
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
        private static readonly HashSet<string> AllowedThemes = new(StringComparer.OrdinalIgnoreCase) { "minimal", "classic", "modern" };

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var resumes = _context.Resumes.Where(r => r.UserId == userId).ToList();
            return View(resumes);
        }

        public IActionResult Create()
        {
            var resume = new Resume { Theme = "minimal" };
            return View(resume);
        }

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
            // Set the current user as the owner
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            resume.UserId = userId;

            // Normalize Theme
            resume.Theme = AllowedThemes.Contains(resume.Theme ?? string.Empty) ? resume.Theme!.ToLowerInvariant() : "minimal";

            resume.Experiences = experiences ?? new List<Experience>();
            resume.EducationHistory = educationHistory ?? new List<Education>();
            resume.Projects = projects ?? new List<Project>();
            resume.Certifications = certifications ?? new List<Certification>();
            resume.Languages = languages ?? new List<Language>();

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
                return RedirectToAction("View", new { id = resume.Id, theme = resume.Theme });
            }

            return View(resume);
        }

        public IActionResult Edit(int? resumeId)
        {
            if (resumeId == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var resume = _context.Resumes
                .Include(r => r.Experiences)
                .Include(r => r.EducationHistory)
                .Include(r => r.Certifications)
                .Include(r => r.Projects)
                .Include(r => r.Languages)
                .FirstOrDefault(r => r.Id == resumeId && r.UserId == userId);

            if (resume == null)
            {
                return NotFound();
            }

            return View(resume);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(
            Resume resume,
            string skillsInput,
            List<Experience> experiences,
            List<Education> educationHistory,
            List<Project> projects,
            List<Certification> certifications,
            List<Language> languages)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existing = _context.Resumes
                .Include(r => r.Experiences)
                .Include(r => r.EducationHistory)
                .Include(r => r.Certifications)
                .Include(r => r.Projects)
                .Include(r => r.Languages)
                .FirstOrDefault(r => r.Id == resume.Id && r.UserId == userId);

            if (existing == null)
            {
                return NotFound();
            }

            existing.FullName = resume.FullName;
            existing.Email = resume.Email;
            existing.CellPhoneNumber = resume.CellPhoneNumber;
            existing.Address = resume.Address;
            existing.Summary = resume.Summary;
            existing.Theme = AllowedThemes.Contains(resume.Theme ?? string.Empty) ? resume.Theme!.ToLowerInvariant() : (existing.Theme ?? "minimal");

            List<string> normalizedSkills = new();
            if (!string.IsNullOrWhiteSpace(skillsInput))
            {
                normalizedSkills = skillsInput
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .ToList();
            }
            existing.Skills = normalizedSkills;

            if ((experiences == null || experiences.Count == 0))
            {
                ModelState.AddModelError(string.Empty, "At least one work experience is required.");
            }
            if ((educationHistory == null || educationHistory.Count == 0))
            {
                ModelState.AddModelError(string.Empty, "At least one education entry is required.");
            }
            if (normalizedSkills.Count < 3)
            {
                ModelState.AddModelError(string.Empty, "Please enter at least 3 skills.");
            }

            if (!ModelState.IsValid)
            {
                resume.Experiences = existing.Experiences;
                resume.EducationHistory = existing.EducationHistory;
                resume.Projects = existing.Projects;
                resume.Certifications = existing.Certifications;
                resume.Languages = existing.Languages;
                resume.Skills = existing.Skills;
                resume.Theme = existing.Theme;
                return View(resume);
            }

            void Sync<T>(ICollection<T> current, List<T> posted, Func<T, int> getId) where T : class
            {
                posted ??= new List<T>();
                var postedIds = posted.Where(p => getId(p) > 0).Select(getId).ToHashSet();
                var toRemove = current.Where(c => !postedIds.Contains(getId(c))).ToList();
                foreach (var rem in toRemove)
                    current.Remove(rem);
                foreach (var item in posted)
                {
                    var id = getId(item);
                    if (id == 0)
                    {
                        current.Add(item);
                    }
                    else
                    {
                        var existingItem = current.FirstOrDefault(c => getId(c) == id);
                        if (existingItem != null)
                        {
                            _context.Entry(existingItem).CurrentValues.SetValues(item);
                        }
                    }
                }
            }

            Sync(existing.Experiences, experiences, e => e.Id);
            foreach (var e in existing.Experiences) e.ResumeId = existing.Id;
            Sync(existing.EducationHistory, educationHistory, e => e.Id);
            foreach (var e in existing.EducationHistory) e.ResumeId = existing.Id;
            Sync(existing.Projects, projects, p => p.Id);
            foreach (var p in existing.Projects) p.ResumeId = existing.Id;
            Sync(existing.Certifications, certifications, c => c.Id);
            foreach (var c in existing.Certifications) c.ResumeId = existing.Id;
            Sync(existing.Languages, languages, l => l.Id);
            foreach (var l in existing.Languages) l.ResumeId = existing.Id;

            _context.SaveChanges();
            return RedirectToAction("View", new { id = existing.Id, theme = existing.Theme });
        }

        public IActionResult View(int? id, string? theme)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var resume = _context.Resumes
                .Include(r => r.Experiences)
                .Include(r => r.EducationHistory)
                .Include(r => r.Certifications)
                .Include(r => r.Projects)
                .Include(r => r.Languages)
                .FirstOrDefault(r => r.Id == id && r.UserId == userId);

            if (resume == null)
            {
                return NotFound();
            }

            var chosen = !string.IsNullOrWhiteSpace(theme) && AllowedThemes.Contains(theme)
                ? theme!.ToLowerInvariant()
                : (AllowedThemes.Contains(resume.Theme ?? string.Empty) ? resume.Theme!.ToLowerInvariant() : "minimal");
            ViewBag.Theme = chosen;

            return View(resume);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var resume = _context.Resumes.FirstOrDefault(r => r.Id == id && r.UserId == userId);
            
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
