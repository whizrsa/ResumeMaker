using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ResumeMaker.Models
{
    public class Language
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Language Name")]
        [MaxLength(100)]
        public string LanguageName { get; set; }

        [Required]
        [Display(Name = "Proficiency Level")]
        [MaxLength(50)]
        public string ProficiencyLevel { get; set; } // e.g., Native, Fluent, Intermediate, Basic

        // Foreign key to Resume
        public int ResumeId { get; set; }

        [ValidateNever]
        public Resume? Resume { get; set; }
    }
}
