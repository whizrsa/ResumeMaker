using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ResumeMaker.Models
{
    public class Experience
    {
        public int Id { get; set; }

        [Display(Name = "Job Title")]
        [MaxLength(100)]
        public string JobTitle { get; set; }

        [Display(Name = "Company Name")]
        [MaxLength(100)]
        public string Company { get; set; }

        [Display(Name = "Location")]
        [MaxLength(255)]
        public string Location { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; } // Nullable for current jobs

        [Display(Name = "Description")]
        [MaxLength(500)]
        public string Description { get; set; }

        public int ResumeId { get; set; }

        [ValidateNever]
        public Resume? Resume { get; set; }
    }
}
