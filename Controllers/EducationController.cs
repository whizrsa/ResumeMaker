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
    }
}
