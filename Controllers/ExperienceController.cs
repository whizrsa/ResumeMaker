using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeMaker.Data;
using ResumeMaker.Models;

namespace ResumeMaker.Controllers
{
    [Authorize]
    public class ExperienceController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ExperienceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create(int resumeId)
        {
            var experience = new Experience {
                ResumeId = resumeId,
                StartDate = DateTime.Today
            };
            return View(experience);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Experience experience)
        {
            if (ModelState.IsValid)
            {
                _context.Experiences.Add(experience);
                _context.SaveChanges();
                return RedirectToAction("Edit", "Home", new { resumeId = experience.ResumeId });
            }

            return View(experience);
        }
    }
}
