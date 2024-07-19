using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        public User? User { get; set; }


        [Required(ErrorMessage = "College ID is required")]
        public int CollegeId { get; set; }
        
        public College? College { get; set; }


        [Required(ErrorMessage = "Degree Name is required")]
        public string DegreeName { get; set; }

        [Required(ErrorMessage = "Twelfth Percentage is required")]
        public double TwelfthPercentage { get; set; }

        [Required(ErrorMessage = "Previous College is required")]
        public string PreviousCollege { get; set; }

        [Required(ErrorMessage = "Previous College CGPA is required")]
        public double PreviousCollegeCGPA { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Created At is required")]
        public DateTime CreatedAt { get; set; }
    }
}
