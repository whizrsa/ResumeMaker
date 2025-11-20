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

        // GET: Experience/Edit/5
        public IActionResult Edit(int id)
        {
            var exp = _context.Experiences.Find(id);
            if (exp == null)
            {
                return NotFound();
            }
            return View(exp);
        }

        // POST: Experience/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Experience model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var exp = _context.Experiences.Find(model.Id);
            if (exp == null)
            {
                return NotFound();
            }

            exp.JobTitle = model.JobTitle;
            exp.Company = model.Company;
            exp.Location = model.Location;
            exp.StartDate = model.StartDate;
            exp.EndDate = model.EndDate;
            exp.Description = model.Description;
            _context.SaveChanges();
            return RedirectToAction("Edit", "Home", new { resumeId = exp.ResumeId });
        }

        // POST: Experience/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var exp = _context.Experiences.Find(id);
            if (exp == null)
            {
                return NotFound();
            }
            var resumeId = exp.ResumeId;
            _context.Experiences.Remove(exp);
            _context.SaveChanges();
            return RedirectToAction("Edit", "Home", new { resumeId });
        }
    }
}
