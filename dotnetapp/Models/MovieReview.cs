// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;

// namespace dotnetapp.Models
// {
//     public class HistoricalTour
//     {
//         public int HistoricalTourID { get; set; }

//         [Required(ErrorMessage = "TourName is required")]
//         public string TourName { get; set; }

//         [Required(ErrorMessage = "StartTime is required")]
//         public string StartTime { get; set; }

//         [Required(ErrorMessage = "EndTime is required")]
//         public string EndTime { get; set; }

//         [Required(ErrorMessage = "Capacity is required")]
//         public int Capacity { get; set; }

//         [Required(ErrorMessage = "Location is required")]
//         public string Location { get; set; }

//         public string Description { get; set; }

//         public virtual ICollection<Participant> Participants { get; set; }
//     }
// }



using System;
using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
public class MovieReview
    {
        public int MovieReviewID { get; set; }

        [Required(ErrorMessage = "MovieID is required")]
        public int MovieID { get; set; }

        [Required(ErrorMessage = "ReviewerName is required")]
        public string ReviewerName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "ReviewText is required")]
        public string ReviewText { get; set; }

        [Required(ErrorMessage = "ReviewDate is required")]
        public DateTime ReviewDate { get; set; }

        public virtual Movie? Movie { get; set; }
    }
}
