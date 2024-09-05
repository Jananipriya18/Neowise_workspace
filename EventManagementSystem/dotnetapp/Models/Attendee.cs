using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
   public class Attendee
    {
        public int AttendeeId { get; set; }  // Primary key
        public string Name { get; set; }  // Name of the attendee
        public string Email { get; set; }  // Email of the attendee
        public int? EventId { get; set; }  // Foreign key for the event
        public Event? Event { get; set; }  // Navigation property to Event
    }
}
