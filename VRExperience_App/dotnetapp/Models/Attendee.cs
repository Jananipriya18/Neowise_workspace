using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Attendee
    {
        public int AttendeeID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        public int VRExperienceID { get; set; }

        public virtual VRExperience? VRExperience { get; set; }
    }
}
