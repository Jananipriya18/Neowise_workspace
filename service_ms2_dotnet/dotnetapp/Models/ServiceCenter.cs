using System.Text.Json.Serialization;

namespace dotnetapp.Models
{
    public class ServiceCenter
    {
        public int ServiceCenterId { get; set; }  // Primary key
        public string Name { get; set; }  // Required, represents the name of the service center
        public string Location { get; set; }  // Required, represents the location of the service center
        public string ContactInfo { get; set; }  // Required, contains phone or email contact details
        [JsonIgnore]
        public ICollection<ServiceBooking> ServiceBookings { get; set; }  // Navigation property
    }
}
