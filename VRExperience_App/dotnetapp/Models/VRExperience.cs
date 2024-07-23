using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class VRExperience
    {
        public int VRExperienceID { get; set; }

        [Required(ErrorMessage = "ExperienceName is required")]
        public string ExperienceName { get; set; }

        [Required(ErrorMessage = "StartTime is required")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "EndTime is required")]
        public string EndTime { get; set; }

        [Required(ErrorMessage = "MaxCapacity is required")]
        public int MaxCapacity { get; set; } 
        public string Location { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Attendee> Attendees { get; set; }
    }
}
