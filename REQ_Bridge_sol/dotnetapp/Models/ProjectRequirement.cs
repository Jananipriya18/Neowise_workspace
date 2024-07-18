using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetapp.Models
{
    public class ProjectRequirement
    {
        [Key]
        public int RequirementId { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }

        public User? User { get; set; }

        [Required(ErrorMessage = "Project requirement title is required")]
        public string RequirementTitle { get; set; }

        [Required(ErrorMessage = "Project requirement description is required")]
        public string RequirementDescription { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }
    }
}
