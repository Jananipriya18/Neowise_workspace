using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Workout
    {
        [Key]
        public int WorkoutId { get; set; }

        [Required]

        public string WorkoutName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int DifficultyLevel  { get; set; } 

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string TargetArea { get; set; } // e.g., "Full Body", "Upper Body", "Lower Body", "Core"

        [Required]
        public int DaysPerWeek { get; set; } // e.g., 3 days per week

        [Required]
        public int AverageWorkoutDurationInMinutes { get; set; } // e.g., 45 minutes per workout
    }
}
