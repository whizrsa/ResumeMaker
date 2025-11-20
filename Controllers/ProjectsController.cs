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

        public IActionResult Edit(int id)
        {
            var prj = _context.Projects.Find(id);
            if (prj == null) return NotFound();
            return View(prj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Project model)
        {
            if (!ModelState.IsValid) return View(model);
            var prj = _context.Projects.Find(model.Id);
            if (prj == null) return NotFound();

            prj.ProjectName = model.ProjectName;
            prj.Description = model.Description;
            prj.Technologies = model.Technologies;
            prj.ProjectUrl = model.ProjectUrl;
            prj.StartDate = model.StartDate;
            prj.EndDate = model.EndDate;
            _context.SaveChanges();
            return RedirectToAction("Edit", "Home", new { resumeId = prj.ResumeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var prj = _context.Projects.Find(id);
            if (prj == null) return NotFound();
            var resumeId = prj.ResumeId;
            _context.Projects.Remove(prj);
            _context.SaveChanges();
            return RedirectToAction("Edit", "Home", new { resumeId });
        }
    }
}
