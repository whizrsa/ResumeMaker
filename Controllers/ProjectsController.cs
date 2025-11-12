using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeMaker.Data;
using ResumeMaker.Models;

namespace ResumeMaker.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create(int resumeId)
        {
            var project = new Project
            {
                ResumeId = resumeId,
                StartDate = DateTime.Today
            };
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Projects.Add(project);
                _context.SaveChanges();
                return RedirectToAction("Edit", "Home", new { resumeId = project.ResumeId });
            }

            return View(project);
        }
    }
}
