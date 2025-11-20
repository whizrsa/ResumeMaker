using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResumeMaker.Data;
using ResumeMaker.Models;

namespace ResumeMaker.Controllers
{
    [Authorize]
    public class CertificationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CertificationsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Create(int resumeId)
        {
            var certification = new Certification
            {
                ResumeId = resumeId,
                DateAcquired = DateTime.Today
            };
            return View(certification);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Certification certification)
        {
            if(ModelState.IsValid)
            {
                _context.Certifications.Add(certification);
                _context.SaveChanges();
                return RedirectToAction("Edit", "Home", new { resumeId = certification.ResumeId });
            }

            return View(certification);
        }

        public IActionResult Edit(int id)
        {
            var cert = _context.Certifications.Find(id);
            if (cert == null) return NotFound();
            return View(cert);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Certification model)
        {
            if (!ModelState.IsValid) return View(model);
            var cert = _context.Certifications.Find(model.Id);
            if (cert == null) return NotFound();
            cert.CertificationName = model.CertificationName;
            cert.DateAcquired = model.DateAcquired;
            _context.SaveChanges();
            return RedirectToAction("Edit", "Home", new { resumeId = cert.ResumeId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var cert = _context.Certifications.Find(id);
            if (cert == null) return NotFound();
            var resumeId = cert.ResumeId;
            _context.Certifications.Remove(cert);
            _context.SaveChanges();
            return RedirectToAction("Edit", "Home", new { resumeId });
        }
    }
}
