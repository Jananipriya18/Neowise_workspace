using System.ComponentModel.DataAnnotations;

namespace dotnetapp.Models
{
    public class Event
    {
        public int eventId { get; set; }

        [Required(ErrorMessage = "Event name is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Event name must be between 1 and 100 characters.")]
        public string eventName { get; set; }

        [Required(ErrorMessage = "Event description is required.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Event description must be between 1 and 500 characters.")]
        public string eventDescription { get; set; }

        [Required(ErrorMessage = "Event date is required.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Event date must be in the format YYYY-MM-DD.")]
        public string eventDate { get; set; }

        [Required(ErrorMessage = "Event time is required.")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Event time must be in the format HH:MM.")]
        public string eventTime { get; set; }

        [Required(ErrorMessage = "Event location is required.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Event location must be between 1 and 200 characters.")]
        public string eventLocation { get; set; }

        [Required(ErrorMessage = "Event organizer is required.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Event organizer must be between 1 and 100 characters.")]
        public string eventOrganizer { get; set; }
    }
}
