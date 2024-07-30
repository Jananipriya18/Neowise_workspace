using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Participant
    {
        public int ParticipantID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "HistoricalTourID is required")]
        public int HistoricalTourID { get; set; }

        public virtual HistoricalTour? HistoricalTour { get; set; }
    }
}
