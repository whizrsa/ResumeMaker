using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ResumeMaker.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        [MaxLength(200)]
        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "Description")]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Display(Name = "Technologies Used")]
        [MaxLength(500)]
        public string Technologies { get; set; }

        [Display(Name = "Project URL")]
        [MaxLength(500)]
        [Url]
        public string? ProjectUrl { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        // Foreign key to Resume
        public int ResumeId { get; set; }

        [ValidateNever]
        public Resume? Resume { get; set; }
    }
}
