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
    }
}
