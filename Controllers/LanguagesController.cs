using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeMaker.Data;
using ResumeMaker.Models;

namespace ResumeMaker.Controllers
{
    [Authorize]
    public class LanguagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LanguagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create(int resumeId)
        {
            var language = new Language
            {
                ResumeId = resumeId
            };
            return View(language);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Language language)
        {
            if (ModelState.IsValid)
            {
                _context.Languages.Add(language);
                _context.SaveChanges();
                return RedirectToAction("Edit", "Home", new { resumeId = language.ResumeId });
            }

            return View(language);
        }
    }
}
