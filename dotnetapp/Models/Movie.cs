// using System;
// using System.ComponentModel.DataAnnotations;

// namespace dotnetapp.Models
// {
//     public class Participant
//     {
//         public int ParticipantID { get; set; }

//         [Required(ErrorMessage = "Name is required")]
//         public string Name { get; set; }

//         [Required(ErrorMessage = "Email is required")]
//         [EmailAddress(ErrorMessage = "Invalid Email Address")]
//         public string Email { get; set; }

//         [Required(ErrorMessage = "PhoneNumber is required")]
//         [Phone(ErrorMessage = "Invalid Phone Number")]
//         public string PhoneNumber { get; set; }

//         [Required(ErrorMessage = "HistoricalTourID is required")]
//         public int HistoricalTourID { get; set; }

//         public virtual HistoricalTour? HistoricalTour { get; set; }
//     }
// }

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Movie
    {
        public int MovieID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Director is required")]
        public string Director { get; set; }

        [Required(ErrorMessage = "ReleaseYear is required")]
        public DateTime ReleaseYear { get; set; }

        public virtual ICollection<MovieReview>? Reviews { get; set; }
    }
}