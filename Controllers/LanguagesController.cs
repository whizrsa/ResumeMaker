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

        public IActionResult Edit(int id)
        {
            var lang = _context.Languages.Find(id);
            if (lang == null) return NotFound();
            return View(lang);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Language model)
        {
            if (!ModelState.IsValid) return View(model);
            var lang = _context.Languages.Find(model.Id);
            if (lang == null) return NotFound();
            lang.LanguageName = model.LanguageName;
            lang.ProficiencyLevel = model.ProficiencyLevel;
            _context.SaveChanges();
            return RedirectToAction("Edit", "Home", new { resumeId = lang.ResumeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var lang = _context.Languages.Find(id);
            if (lang == null) return NotFound();
            var resumeId = lang.ResumeId;
            _context.Languages.Remove(lang);
            _context.SaveChanges();
            return RedirectToAction("Edit", "Home", new { resumeId });
        }
    }
}
