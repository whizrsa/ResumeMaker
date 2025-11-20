using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ResumeMaker.Models
{
    public class Resume
    {
        public Resume()
        {
            Experiences = new List<Experience>();
            EducationHistory = new List<Education>();
            Certifications = new List<Certification>();
            Projects = new List<Project>();
            Languages = new List<Language>();
            Skills = new List<string>();
            Theme = "minimal"; // default theme
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [MaxLength(255)]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Cellphone Number")]
        [Phone]
        [MaxLength(50)]
        public string CellPhoneNumber { get; set; }

        [Required]
        [Display(Name = "Address")]
        [MaxLength(255)]
        public string Address { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Summary")]
        public string Summary { get; set; }

        [MaxLength(20)]
        [Display(Name = "Resume Theme")]
        public string Theme { get; set; }

        // Foreign key to ApplicationUser - automatically set by controller
        [BindNever]
        public string UserId { get; set; } = string.Empty;

        [ValidateNever]
        public ApplicationUser? User { get; set; }
        
        public List<Experience> Experiences { get; set; }
        public List<Education> EducationHistory { get; set; }
        public List<Certification> Certifications { get; set; }
        public List<Project> Projects { get; set; }
        public List<Language> Languages { get; set; }
        public List<string> Skills { get; set; }
    }
}
