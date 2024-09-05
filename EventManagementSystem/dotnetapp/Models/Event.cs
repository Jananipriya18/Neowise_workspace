using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
   public class Event
    {
        public int EventId { get; set; }  // Primary key
        public string Name { get; set; }  // Name of the event
        public DateTime EventDate { get; set; }  // Date and time of the event
        public string Location { get; set; }  // Location of the event

        // Navigation property for attendees of the event
        [JsonIgnore]
        public ICollection<Attendee>? Attendees { get; set; }
    }
}
