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
