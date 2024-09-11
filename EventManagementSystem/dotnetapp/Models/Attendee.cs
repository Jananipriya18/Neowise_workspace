using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
    public class Attendee
    {
        public int AttendeeId { get; set; }  
        public string Name { get; set; } 
        public string Email { get; set; } 

        public int? EventId { get; set; }  
        public Event? Event { get; set; } 
    }
}
