using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ResumeMaker.Models
{
    public class Education
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Institution Name")]
        public string Institution { get; set; }

        [Required]
        [MaxLength(255)]
        [Display(Name = "Degree Name")]
        public string Degree { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        // Foreign key to Resume
        public int ResumeId { get; set; }

        [ValidateNever]
        public Resume? Resume { get; set; }
    }
}
