using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class WorkoutRequest
    {
        [Key]
        public int WorkoutRequestId { get; set; }

        [Required]
        public int UserId { get; set; }

        public User? User { get; set; }

        [Required]
        public int WorkoutId { get; set; }

        public Workout? Workout { get; set; }

        [Required]
        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        [Range(10, 50)]
        public double BMI { get; set; }

        [Required]
        public string Gender { get; set; } // Options: "Male", "Female", "Other"

        [Required]
        public string DietaryPreferences { get; set; } // e.g., "Vegetarian", "Vegan", "Keto", "No preference"

        [Required]
        public string MedicalHistory { get; set; } // e.g., "Asthma", "Hypertension", "None"

        [Required]
        public DateTime RequestedDate { get; set; } = DateTime.UtcNow;

        [Required]
        public string RequestStatus { get; set; } // e.g., "Pending", "Approved", "Rejected"
    }
}
