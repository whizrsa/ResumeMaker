using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ResumeMaker.Models
{
    public class Certification
    {
        public int Id { get; set; }

        [Display(Name = "Certification Name")]
        [MaxLength(255)]
        public string CertificationName { get; set; }

        public DateTime DateAcquired { get; set; }

        // Foreign key to Resume
        public int ResumeId { get; set; }

        [ValidateNever]
        public Resume? Resume { get; set; }
    }
}
