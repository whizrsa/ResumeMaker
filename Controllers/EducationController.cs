using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeMaker.Data;
using ResumeMaker.Models;

namespace ResumeMaker.Controllers
{
    [Authorize]
    public class EducationController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EducationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create(int resumeId)
        {
            var education = new Education
            {
                ResumeId = resumeId,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };
            return View(education);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Education education)
        {
            if(ModelState.IsValid)
            {
                _context.Educations.Add(education);
                _context.SaveChanges();
                return RedirectToAction("Edit", "Home", new { resumeId = education.ResumeId });
            }

            return View(education);
        }

        public IActionResult Edit(int id)
        {
            var edu = _context.Educations.Find(id);
            if (edu == null) return NotFound();
            return View(edu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Education model)
        {
            if (!ModelState.IsValid) return View(model);
            var edu = _context.Educations.Find(model.Id);
            if (edu == null) return NotFound();
            edu.Institution = model.Institution;
            edu.Degree = model.Degree;
            edu.StartDate = model.StartDate;
            edu.EndDate = model.EndDate;
            _context.SaveChanges();
            return RedirectToAction("Edit", "Home", new { resumeId = edu.ResumeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var edu = _context.Educations.Find(id);
            if (edu == null) return NotFound();
            var resumeId = edu.ResumeId;
            _context.Educations.Remove(edu);
            _context.SaveChanges();
            return RedirectToAction("Edit", "Home", new { resumeId });
        }
    }
}
