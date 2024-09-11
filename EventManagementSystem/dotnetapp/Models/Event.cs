using System.Collections.Generic; 
using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
    public class Event
    {
        public int EventId { get; set; } 
        public string Name { get; set; }  
        public string EventDate { get; set; } 
        public string Location { get; set; } 

        [JsonIgnore]
        public ICollection<Attendee>? Attendees { get; set; }  
    }
}
